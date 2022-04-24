using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaz_Carro.Clases
{
    public class Carro
    {
        public string Marca { get; }
        public string Color { get; set; } = " ";
        public int Modelo { get; }
        public string dueño { get; set; } = " ";
        public int MAXKPH { get; }

        private int Encendido = 0;
        private int Encendido2 = 0;
        private int velocidad_actual = 0;

        //CONSTRUCTOR
        public Carro(string _marca, int _maxvel, string _dueño, string color)
        {
            Marca = _marca;
            MAXKPH = _maxvel;
            dueño = _dueño;
            Color = color;
            this.velocidad_actual = 0;
        }
        public string EncenderMotor()
        {
            string mensaje = " ";

            if (Encendido == 0)
            {
                velocidad_actual = 0;
                mensaje = "Carro encendido";
                Encendido = 1;
                Encendido2 = 1;

            }
            else
            {
                mensaje = "El carro ya estaba encendido";
            }

            return mensaje;
        }
        public string acelerar()
        {
            if (Encendido2 == 0)
            {
                return $"El carro esta apagado";
            }

            string mensaje = "";
            if (velocidad_actual < MAXKPH)
            {
                velocidad_actual += 10;
                if(velocidad_actual > MAXKPH)
                {
                    velocidad_actual = MAXKPH;
                }
                mensaje = $"Has acelerado 10 KPH \nVas a {velocidad_actual} KPH";
            }
            else
            {
                velocidad_actual = MAXKPH;
                mensaje = $"Vas a velocidad maxima {velocidad_actual} KPH no puedes acelerar más \nVas a {velocidad_actual}";
            };
            return mensaje;

        }

        public string desacelerar()
        {
            string mensaje = " ";
            if (velocidad_actual > 15)
            {
                velocidad_actual -= 15;
                mensaje = $"Has desacelerado \nVas a {velocidad_actual} KPH";
            }
            if(velocidad_actual == 0)
            {
                mensaje = $"No se puede desacelerar ya que el carro está detenido";
            }
            else if (velocidad_actual <= 15)
            {
                velocidad_actual = 15;
                mensaje = $"\nNo se puede desacelerar menos de {velocidad_actual} KPH \nVas a {velocidad_actual}";    
            }
            return mensaje;
        }
        public string frenar()
        {
            string mensaje = " ";
            if (velocidad_actual > 0)
            {
                velocidad_actual = velocidad_actual - 10;
                mensaje = $"El carro ha frenado un poco \nSu velocidad es {velocidad_actual}";
                if(velocidad_actual < 0)
                {
                    velocidad_actual = 0;
                }
            }
            if (velocidad_actual == 0)
            {
                mensaje = "Has frenado por completo";
            }
            return mensaje;
        }
        public string bocinar()
        {
            string mensaje = "\nBocina bip bip\n";
            return mensaje;

        }
        public string apagarmotor()
        {
            string mensaje = " ";
            if (Encendido2 == 0)
            {
                return "No se ha encendido el carro";
            }
            if (velocidad_actual != 0)
            {
                mensaje = $"\nNo puede apagar el carro si sigue en movimiento";
            }
            else
            {
                mensaje = $"\nEl carro se ha apagado";
            }
            return mensaje;
        }
    }
}
