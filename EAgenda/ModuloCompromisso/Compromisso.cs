using EAgenda.Compartilhado;
using EAgenda.ModuloContato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAgenda.ModuloCompromisso
{
    public class Compromisso : EntidadeBase
    {
        public Contato contato;
        private DateTime dataCompromisso;

        private bool estaAberto;
        public bool EstaAberto { get => estaAberto; }
        public DateTime DataCompromisso { get => dataCompromisso; }
        public string Status { get => EstaAberto ? "Aberto" : "Fechado"; }

        private readonly string atividade;
        private readonly string local;
        private readonly string horarioInicio;
        private readonly string horarioTermino;


        public string Atividade => atividade;
        public string Local => local;
        public string HorarioInicio => horarioInicio;
        public string HorarioTermino => horarioTermino;



        public Compromisso(string atividade, string local, string horarioInicio, string horarioTermino)
        {
            this.atividade = atividade;
            this.local = local;
            this.horarioInicio = horarioInicio;
            this.horarioTermino = horarioTermino;

            Abrir();
        }

        public Compromisso(Contato contato)
        {
            this.contato = contato;
        }

        public override string ToString()
        {
            return "Número: " + numero + Environment.NewLine +
                "Atividade: " + atividade + Environment.NewLine +
                "Local: " + local + Environment.NewLine +
                "Nome do Contato: " + contato.Nome + Environment.NewLine +
                "Status do compromisso: " + Status + Environment.NewLine +
                //"Data do Compromisso: " + DataCompromisso.ToShortDateString() + Environment.NewLine +
                "Horario de Início: " + horarioInicio + Environment.NewLine +
                "Horario de Termino: " + horarioTermino + Environment.NewLine;

        }

        public void Abrir()
        {
            if (!estaAberto)
            {
                estaAberto = true;
                dataCompromisso = DateTime.Today;

                contato.RegistrarCompromisso(this);
            }
        }

        public void Fechar()
        {
            if (estaAberto)
            {
                estaAberto = false;

                DateTime dataRealDevolucao = DateTime.Today;
            }
        }

        public override ResultadoValidacao Validar()
        {
            return new ResultadoValidacao(new List<string>());
        }
    }
}
