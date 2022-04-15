using EAgenda.Compartilhado;
using EAgenda.ModuloCompromisso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAgenda.ModuloContato
{
    public class Contato : EntidadeBase
    {
        private readonly string nome;
        private readonly string email;
        private readonly string telefone;
        private readonly string endereco;
        private readonly string cargo;

        private readonly List<Compromisso> historicoCompromissos = new List<Compromisso>();

        public string Nome => nome;

        public string Email => email;
        public string Telefone => telefone;
        public string Endereco => endereco;
        public string Cargo => cargo;

        public Contato(string nome, string email, string telefone, string endereco, string cargo)
        {
            this.nome = nome;
            this.email = email;
            this.telefone = telefone;
            this.endereco = endereco;
            this.cargo = cargo;
        }

        public void RegistrarCompromisso(Compromisso compromisso)
        {
            historicoCompromissos.Add(compromisso);
        }

        public bool TemCompromissoEmAberto()
        {
            bool temCompromissoEmAberto = false;

            foreach (Compromisso compromisso in historicoCompromissos)
            {
                if (compromisso.EstaAberto)
                {
                    temCompromissoEmAberto = true;
                    break;
                }
            }

            return temCompromissoEmAberto;
        }

        public override string ToString()
        {
            return "Número: " + numero + Environment.NewLine +
            "Nome: " + Nome + Environment.NewLine +
            "e-mail: " + Email + Environment.NewLine +
            "Telefone: " + Telefone + Environment.NewLine +
            "Endereço: " + Endereco + Environment.NewLine +
            "Cargo: " + Cargo + Environment.NewLine;
        }

        public override ResultadoValidacao Validar()
        {
            List<string> erros = new List<string>();

            if (string.IsNullOrEmpty(nome))
                erros.Add("O contato precisa ter um nome!");

            if (string.IsNullOrEmpty(email))
                erros.Add("O contato precisa ter um e-mail!");

            if (telefone.Length < 9)
                erros.Add("O contato precisa ter um número de telefone com 9 digitos!");

            return new ResultadoValidacao(erros);
        }
    }
}
