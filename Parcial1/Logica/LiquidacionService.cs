using System;
using System.Collections.Generic;
using Datos;
using Entidades;

namespace Logica
{
    public class LiquidacionService
    {
        private readonly LiquidacionRepository repository;
        private readonly Random random;

        public LiquidacionService()
        {
            repository = new LiquidacionRepository();
            random = new Random();
        }

        public Liquidacion CrearLiquidacion(double salarioDevengado, int diasIncapacidad)
        {
            try
            {
                // Validar datos de entrada
                if (salarioDevengado < 1300000)
                {
                    throw new Exception("El salario no puede ser menor al salario mínimo ($1.300.000)");
                }

                if (diasIncapacidad <= 0)
                {
                    throw new Exception("Los días de incapacidad deben ser mayores a cero");
                }

                // Generar número de liquidación único
                int numeroLiquidacion = GenerarNumeroLiquidacionUnico();

                // Crear la liquidación
                Liquidacion liquidacion = new Liquidacion(numeroLiquidacion, salarioDevengado, diasIncapacidad);

                // Guardar la liquidación
                repository.GuardarLiquidacion(liquidacion);

                return liquidacion;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear la liquidación: {ex.Message}");
            }
        }

        public List<Liquidacion> ConsultarLiquidaciones()
        {
            try
            {
                return repository.ConsultarLiquidaciones();
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
                if (!repository.ExisteNumeroLiquidacion(numeroLiquidacion))
                {
                    throw new Exception($"No existe una liquidación con el número {numeroLiquidacion}");
                }

                repository.EliminarLiquidacion(numeroLiquidacion);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar la liquidación: {ex.Message}");
            }
        }

        private int GenerarNumeroLiquidacionUnico()
        {
            int numeroLiquidacion;

            do
            {
                numeroLiquidacion = random.Next(1000, 9999);
            } while (repository.ExisteNumeroLiquidacion(numeroLiquidacion));

            return numeroLiquidacion;
        }
    }
}
