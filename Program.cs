using System;
using System.Collections.Generic;

namespace TodoListApp
{
    // Clase para representar una tarea
    public class Tarea
    {
        public string Descripcion { get; set; }
        public DateTime? FechaLimite { get; set; }
        public bool Completada { get; set; }

        public Tarea(string descripcion, DateTime? fechaLimite = null)
        {
            Descripcion = descripcion;
            FechaLimite = fechaLimite;
            Completada = false;
        }

        public override string ToString()
        {
            string fecha = FechaLimite.HasValue ? FechaLimite.Value.ToShortDateString() : "Sin fecha";
            return $"{Descripcion} (Fecha límite: {fecha}) - {(Completada ? "Completada" : "Pendiente")}";
        }
    }

    // Clase para manejar la lista de tareas
    public class GestorTareas
    {
        private List<Tarea> _tareas;

        public GestorTareas()
        {
            _tareas = new List<Tarea>();
        }

        public void AgregarTarea(string descripcion, DateTime? fechaLimite = null)
        {
            _tareas.Add(new Tarea(descripcion, fechaLimite));
        }

        public void ListarTareas()
        {
            if (_tareas.Count == 0)
            {
                Console.WriteLine("No hay tareas en la lista.");
                return;
            }

            for (int i = 0; i < _tareas.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_tareas[i]}");
            }
        }

        public void MarcarComoCompletada(int indice)
        {
            if (indice < 1 || indice > _tareas.Count)
            {
                Console.WriteLine("Índice de tarea no válido.");
                return;
            }

            _tareas[indice - 1].Completada = true;
            Console.WriteLine("Tarea marcada como completada.");
        }

        public void EliminarTarea(int indice)
        {
            if (indice < 1 || indice > _tareas.Count)
            {
                Console.WriteLine("Índice de tarea no válido.");
                return;
            }

            _tareas.RemoveAt(indice - 1);
            Console.WriteLine("Tarea eliminada.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var gestor = new GestorTareas();
            while (true)
            {
                Console.WriteLine("\nMenú:");
                Console.WriteLine("1. Agregar tarea");
                Console.WriteLine("2. Listar tareas");
                Console.WriteLine("3. Marcar tarea como completada");
                Console.WriteLine("4. Eliminar tarea");
                Console.WriteLine("5. Salir");
                Console.Write("Selecciona una opción: ");

                try
                {
                    int opcion = int.Parse(Console.ReadLine());

                    switch (opcion)
                    {
                        case 1:
                            Console.Write("Descripción de la tarea: ");
                            string descripcion = Console.ReadLine();
                            Console.Write("Fecha límite (dd/MM/yyyy) o presiona Enter para no poner fecha: ");
                            string fechaInput = Console.ReadLine();
                            DateTime? fechaLimite = null;

                            if (!string.IsNullOrEmpty(fechaInput))
                            {
                                fechaLimite = DateTime.ParseExact(fechaInput, "dd/MM/yyyy", null);
                            }

                            gestor.AgregarTarea(descripcion, fechaLimite);
                            Console.WriteLine("Tarea agregada.");
                            break;

                        case 2:
                            gestor.ListarTareas();
                            break;

                        case 3:
                            gestor.ListarTareas();
                            Console.Write("Número de la tarea a marcar como completada: ");
                            int indiceCompletar = int.Parse(Console.ReadLine());
                            gestor.MarcarComoCompletada(indiceCompletar);
                            break;

                        case 4:
                            gestor.ListarTareas();
                            Console.Write("Número de la tarea a eliminar: ");
                            int indiceEliminar = int.Parse(Console.ReadLine());
                            gestor.EliminarTarea(indiceEliminar);
                            break;

                        case 5:
                            return;

                        default:
                            Console.WriteLine("Opción no válida.");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Entrada no válida. Por favor, ingresa un número.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}
