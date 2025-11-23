using System;
using System.Collections.Generic;

namespace BibliotecaDigital
{
    class Libro
    {
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int Anio { get; set; }
        public string Descripcion { get; set; }

        public Libro(string titulo, string autor, int anio, string descripcion)
        {
            Titulo = titulo;
            Autor = autor;
            Anio = anio;
            Descripcion = descripcion;
        }

        public override string ToString()
        {
            return $"{Titulo} - {Autor} ({Anio})";
        }
    }

    class Programa
    {
        static void Main()
        {
            
            List<Libro> libros = new List<Libro>()
            {
                new Libro("C# Basico", "Juan Pérez", 2018, "Aprende fundamentos de C# y programación orientada a objetos."),
                new Libro("Estructuras de Datos", "Ana Gómez", 2020, "Conceptos y ejemplos de estructuras de datos en C#."),
                new Libro("Algoritmos Avanzados", "Luis Ramírez", 2022, "Optimización de algoritmos y complejidad computacional."),
                new Libro("Bases de Datos", "María López", 2019, "Modelado y consultas en bases de datos relacionales."),
                new Libro("Inteligencia Artificial", "Carlos Díaz", 2023, "Introducción a IA y aprendizaje automático.")
            };

            List<string> autores = new List<string> { "Ana Gómez", "Carlos Díaz", "Juan Pérez", "Luis Ramírez", "María López" };
            autores.Sort();

            bool salir = false;
            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("=== Biblioteca Digital ===");
                Console.WriteLine("1. Búsqueda lineal por título");
                Console.WriteLine("2. Búsqueda binaria por autor");
                Console.WriteLine("3. Libro más reciente y más antiguo");
                Console.WriteLine("4. Búsqueda por palabra clave en descripción");
                Console.WriteLine("5. Salir");
                Console.Write("Seleccione una opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Console.Write("\nIngrese el título del libro: ");
                        string tituloBuscar = Console.ReadLine();
                        Libro encontrado = BusquedaLineal(libros, tituloBuscar);
                        if (encontrado != null)
                            Console.WriteLine("Libro encontrado: " + encontrado);
                        else
                            Console.WriteLine("Libro no encontrado.");
                        break;

                    case "2":
                        Console.Write("\nIngrese el autor: ");
                        string autorBuscar = Console.ReadLine();
                        int indexAutor = BusquedaBinaria(autores, autorBuscar);
                        if (indexAutor != -1)
                            Console.WriteLine("Autor encontrado: " + autores[indexAutor]);
                        else
                            Console.WriteLine("Autor no encontrado.");
                        break;

                    case "3":
                        Libro masReciente = LibromasReciente(libros);
                        Libro masAntiguo = LibromasAntiguo(libros);
                        Console.WriteLine($"\nLibro más reciente: {masReciente}");
                        Console.WriteLine($"Libro más antiguo: {masAntiguo}");
                        break;

                    case "4":
                        Console.Write("\nIngrese palabra clave: ");
                        string keyword = Console.ReadLine();
                        List<Libro> coincidencias = BuscarPorDescripcion(libros, keyword);
                        if (coincidencias.Count > 0)
                        {
                            Console.WriteLine("Libros encontrados:");
                            foreach (var libro in coincidencias)
                                Console.WriteLine("- " + libro);
                        }
                        else
                        {
                            Console.WriteLine("No se encontraron coincidencias.");
                        }
                        break;

                    case "5":
                        salir = true;
                        continue;

                    default:
                        Console.WriteLine("Opción inválida.");
                        break;
                }

                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }

        

        static Libro BusquedaLineal(List<Libro> lista, string titulo)
        {
            foreach (var libro in lista)
            {
                if (libro.Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase))
                    return libro;
            }
            return null;
        }

        static int BusquedaBinaria(List<string> lista, string valor)
        {
            int izquierda = 0;
            int derecha = lista.Count - 1;
            while (izquierda <= derecha)
            {
                int medio = (izquierda + derecha) / 2;
                int comparacion = string.Compare(lista[medio], valor, true);
                if (comparacion == 0) return medio;
                else if (comparacion < 0) izquierda = medio + 1;
                else derecha = medio - 1;
            }
            return -1;
        }

        static Libro LibromasReciente(List<Libro> lista)
        {
            Libro reciente = lista[0];
            foreach (var libro in lista)
            {
                if (libro.Anio > reciente.Anio)
                    reciente = libro;
            }
            return reciente;
        }

        static Libro LibromasAntiguo(List<Libro> lista)
        {
            Libro antiguo = lista[0];
            foreach (var libro in lista)
            {
                if (libro.Anio < antiguo.Anio)
                    antiguo = libro;
            }
            return antiguo;
        }

        static List<Libro> BuscarPorDescripcion(List<Libro> lista, string keyword)
        {
            List<Libro> resultados = new List<Libro>();
            foreach (var libro in lista)
            {
                if (libro.Descripcion.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                    resultados.Add(libro);
            }
            return resultados;
        }
    }
}
