using EAgenda.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAgenda.ModuloTarefa
{
    public class Tarefa : EntidadeBase
    {
        private readonly string titulo;
        private readonly string dataInicio;
        private readonly string dataFinal;
        private readonly string item;

        public string Titulo => titulo;
        public string DataInicio => dataInicio;
        public string DataFinal => dataFinal;
        public string Item => Item;

        public Tarefa(string titulo, string dataInicio, string dataFinal, string item)
        {
            this.titulo = titulo;
            this.dataInicio = dataInicio;
            this.dataFinal = dataFinal;
            this.item = item;
        }

        public override string ToString()
        {
            Console.WriteLine("Quantidade de itens que deseja cadastrar: ");
            string numeroItens = Console.ReadLine();

            return "Titulo: " + titulo + Environment.NewLine +
            "Data de Inicio: " + dataInicio + Environment.NewLine +
            "Data Final: " + dataFinal + Environment.NewLine + 
            "Item: " + item + Environment.NewLine;
        }

        public override ResultadoValidacao Validar()
        {
            throw new NotImplementedException();
        }
    }
}
