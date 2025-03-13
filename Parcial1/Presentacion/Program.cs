using System;
using System.Collections.Generic;
using Entidades;
using Logica;

namespace Presentacion
{
    class Program
    {
        private static LiquidacionService liquidacionService = new LiquidacionService();

        static void Main(string[] args)
        {
            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("============= LIQUIDADOR DE INCAPACIDADES =============");
                Console.WriteLine("1. Registrar nueva liquidación");
                Console.WriteLine("2. Consultar liquidaciones");
                Console.WriteLine("3. Eliminar liquidación");
                Console.WriteLine("4. Salir");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        RegistrarLiquidacion();
                        break;

                    case "2":
                        ConsultarLiquidaciones();
                        break;

                    case "3":
                        EliminarLiquidacion();
                        break;

                    case "4":
                        salir = true;
                        break;

                    default:
                        Console.WriteLine("Opción no válida. Presione cualquier tecla para continuar...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void RegistrarLiquidacion()
        {
            Console.Clear();
            Console.WriteLine("============= REGISTRAR LIQUIDACIÓN =============");

            try
            {
                // Solicitar datos
                Console.Write("Ingrese el salario devengado: ");
                double salarioDevengado = double.Parse(Console.ReadLine());

                Console.Write("Ingrese los días de incapacidad: ");
                int diasIncapacidad = int.Parse(Console.ReadLine());

                // Crear liquidación
                Liquidacion liquidacion = liquidacionService.CrearLiquidacion(salarioDevengado, diasIncapacidad);

                // Mostrar resultado
                Console.WriteLine("\nLiquidación registrada exitosamente:");
                Console.WriteLine("Número de liquidación: " + liquidacion.NumeroLiquidacion);
                Console.WriteLine("Obligado a pagar: " + liquidacion.ObligadoPagar);
                Console.WriteLine("Valor a pagar: $" + liquidacion.ValorAPagar.ToString("N2"));

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        static void ConsultarLiquidaciones()
        {
            Console.Clear();
            Console.WriteLine("============= CONSULTAR LIQUIDACIONES =============");

            try
            {
                List<Liquidacion> liquidaciones = liquidacionService.ConsultarLiquidaciones();

                if (liquidaciones.Count == 0)
                {
                    Console.WriteLine("No hay liquidaciones registradas.");
                }
                else
                {
                    MostrarEncabezadoTabla();

                    foreach (var liquidacion in liquidaciones)
                    {
                        MostrarLiquidacion(liquidacion);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        static void EliminarLiquidacion()
        {
            Console.Clear();
            Console.WriteLine("============= ELIMINAR LIQUIDACIÓN =============");

            try
            {
                // Mostrar liquidaciones actuales
                Console.WriteLine("Liquidaciones actuales:");
                List<Liquidacion> liquidaciones = liquidacionService.ConsultarLiquidaciones();

                if (liquidaciones.Count == 0)
                {
                    Console.WriteLine("No hay liquidaciones registradas.");
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                    return;
                }

                MostrarEncabezadoTabla();

                foreach (var liquidacion in liquidaciones)
                {
                    MostrarLiquidacion(liquidacion);
                }

                // Solicitar número de liquidación a eliminar
                Console.Write("\nIngrese el número de liquidación a eliminar: ");
                int numeroLiquidacion = int.Parse(Console.ReadLine());

                // Eliminar liquidación
                liquidacionService.EliminarLiquidacion(numeroLiquidacion);

                // Mostrar liquidaciones actualizadas
                Console.WriteLine("\nLiquidación eliminada exitosamente.");
                Console.WriteLine("\nLiquidaciones actualizadas:");

                liquidaciones = liquidacionService.ConsultarLiquidaciones();

                if (liquidaciones.Count == 0)
                {
                    Console.WriteLine("No hay liquidaciones registradas.");
                }
                else
                {
                    MostrarEncabezadoTabla();

                    foreach (var liquidacion in liquidaciones)
                    {
                        MostrarLiquidacion(liquidacion);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        static void MostrarEncabezadoTabla()
        {
            Console.WriteLine(new string('-', 135));
            Console.WriteLine("| {0,-5} | {1,-10} | {2,-5} | {3,-15} | {4,-10} | {5,-10} | {6,-8} | {7,-10} | {8,-10} | {9,-10} |",
                "Num", "Salario", "Días", "Obligado", "Sal.Diario", "Dejado", "%", "Calculado", "SMLMD", "A Pagar");
            Console.WriteLine(new string('-', 135));
        }

        static void MostrarLiquidacion(Liquidacion liquidacion)
        {
            Console.WriteLine("| {0,-5} | {1,-10:N0} | {2,-5} | {3,-15} | {4,-10:N2} | {5,-10:N2} | {6,-8:N2} | {7,-10:N2} | {8,-10:N2} | {9,-10:N2} |",
                liquidacion.NumeroLiquidacion,
                liquidacion.SalarioDevengado,
                liquidacion.DiasIncapacidad,
                liquidacion.ObligadoPagar,
                liquidacion.SalarioDiario,
                liquidacion.ValorDejadoPercibir,
                liquidacion.PorcentajeAplicado,
                liquidacion.ValorCalculadoIncapacidad,
                liquidacion.ValorIncapacidadSMLMD,
                liquidacion.ValorAPagar);
        }
    }
}