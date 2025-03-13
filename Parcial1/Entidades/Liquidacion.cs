
using System;
using System.Collections.Generic;
using System.IO;

namespace Entidades
{
    public class Liquidacion
    {
        public int NumeroLiquidacion { get; set; }
        public double SalarioDevengado { get; set; }
        public int DiasIncapacidad { get; set; }
        public string ObligadoPagar { get; set; }
        public double SalarioDiario { get; set; }
        public double ValorDejadoPercibir { get; set; }
        public double PorcentajeAplicado { get; set; }
        public double ValorCalculadoIncapacidad { get; set; }
        public double ValorIncapacidadSMLMD { get; set; }
        public double ValorAPagar { get; set; }

        private const double SALARIO_MINIMO = 1300000.00;
        private const double SALARIO_MINIMO_DIARIO = 43333.33;

        public Liquidacion()
        {
        }

        public Liquidacion(int numeroLiquidacion, double salarioDevengado, int diasIncapacidad)
        {
            NumeroLiquidacion = numeroLiquidacion;
            SalarioDevengado = salarioDevengado;
            DiasIncapacidad = diasIncapacidad;

            CalcularLiquidacion();
        }

        public void CalcularLiquidacion()
        {
            // Calcular salario diario
            SalarioDiario = Math.Round(SalarioDevengado / 30, 2);

            // Calcular valor dejado de percibir
            ValorDejadoPercibir = Math.Round(SalarioDiario * DiasIncapacidad, 2);

            // Determinar el obligado a pagar y el porcentaje aplicado
            DeterminarObligadoPagar();

            // Calcular el valor de la incapacidad
            CalcularValorIncapacidad();

            // Verificar que el valor no sea inferior al salario mínimo diario
            VerificarValorMinimo();
        }

        private void DeterminarObligadoPagar()
        {
            if (DiasIncapacidad >= 1 && DiasIncapacidad <= 2)
            {
                ObligadoPagar = "Empleador";
                PorcentajeAplicado = 66.66;
            }
            else if (DiasIncapacidad >= 3 && DiasIncapacidad <= 90)
            {
                ObligadoPagar = "EPS";

                if (DiasIncapacidad >= 3 && DiasIncapacidad <= 15)
                {
                    PorcentajeAplicado = 66.66;
                }
                else // DiasIncapacidad >= 16 && DiasIncapacidad <= 90
                {
                    PorcentajeAplicado = 60.00;
                }
            }
            else if (DiasIncapacidad >= 91 && DiasIncapacidad <= 540)
            {
                ObligadoPagar = "Fondo de Pensiones";
                PorcentajeAplicado = 50.00;
            }
            else
            {
                ObligadoPagar = "No determinado";
                PorcentajeAplicado = 0.00;
            }
        }

        private void CalcularValorIncapacidad()
        {
            ValorCalculadoIncapacidad = Math.Round(ValorDejadoPercibir * (PorcentajeAplicado / 100), 2);
            ValorIncapacidadSMLMD = Math.Round(SALARIO_MINIMO_DIARIO * DiasIncapacidad, 2);
        }

        private void VerificarValorMinimo()
        {
            // El valor a pagar no puede ser inferior al salario mínimo diario proporcional
            ValorAPagar = Math.Max(ValorCalculadoIncapacidad, ValorIncapacidadSMLMD);
        }

        // Para convertir a string para guardar en archivo
        public string ToFileString()
        {
            return $"{NumeroLiquidacion};{SalarioDevengado};{DiasIncapacidad};{ObligadoPagar};{SalarioDiario};{ValorDejadoPercibir};{PorcentajeAplicado};{ValorCalculadoIncapacidad};{ValorIncapacidadSMLMD};{ValorAPagar}";
        }
    }
}
