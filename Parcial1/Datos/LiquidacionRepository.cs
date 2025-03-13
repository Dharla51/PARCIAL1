using System;
using System.Collections.Generic;
using System.IO;
using Entidades;

namespace Datos
{
    public class LiquidacionRepository
    {
        private readonly string fileName = "liquidaciones.txt";

        public LiquidacionRepository()
        {
            // Crear el archivo si no existe
            if (!File.Exists(fileName))
            {
                File.Create(fileName).Close();
            }
        }

        public void GuardarLiquidacion(Liquidacion liquidacion)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName, true))
                {
                    writer.WriteLine(liquidacion.ToFileString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al guardar la liquidación: {ex.Message}");
            }
        }

        public List<Liquidacion> ConsultarLiquidaciones()
        {
            List<Liquidacion> liquidaciones = new List<Liquidacion>();

            try
            {
                if (File.Exists(fileName))
                {
                    string[] lineas = File.ReadAllLines(fileName);

                    foreach (var linea in lineas)
                    {
                        if (!string.IsNullOrEmpty(linea))
                        {
                            string[] datos = linea.Split(';');

                            Liquidacion liquidacion = new Liquidacion
                            {
                                NumeroLiquidacion = int.Parse(datos[0]),
                                SalarioDevengado = double.Parse(datos[1]),
                                DiasIncapacidad = int.Parse(datos[2]),
                                ObligadoPagar = datos[3],
                                SalarioDiario = double.Parse(datos[4]),
                                ValorDejadoPercibir = double.Parse(datos[5]),
                                PorcentajeAplicado = double.Parse(datos[6]),
                                ValorCalculadoIncapacidad = double.Parse(datos[7]),
                                ValorIncapacidadSMLMD = double.Parse(datos[8]),
                                ValorAPagar = double.Parse(datos[9])
                            };

                            liquidaciones.Add(liquidacion);
                        }
                    }
                }

                return liquidaciones;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al consultar las liquidaciones: {ex.Message}");
            }
        }

        public void EliminarLiquidacion(int numeroLiquidacion)
        {
            try
            {
                List<Liquidacion> liquidaciones = ConsultarLiquidaciones();
                List<Liquidacion> liquidacionesActualizadas = new List<Liquidacion>();

                foreach (var liquidacion in liquidaciones)
                {
                    if (liquidacion.NumeroLiquidacion != numeroLiquidacion)
                    {
                        liquidacionesActualizadas.Add(liquidacion);
                    }
                }

                // Reescribir el archivo con las liquidaciones actualizadas
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    foreach (var liquidacion in liquidacionesActualizadas)
                    {
                        writer.WriteLine(liquidacion.ToFileString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar la liquidación: {ex.Message}");
            }
        }

        public bool ExisteNumeroLiquidacion(int numeroLiquidacion)
        {
            List<Liquidacion> liquidaciones = ConsultarLiquidaciones();

            foreach (var liquidacion in liquidaciones)
            {
                if (liquidacion.NumeroLiquidacion == numeroLiquidacion)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
