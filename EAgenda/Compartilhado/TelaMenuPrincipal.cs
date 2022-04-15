using EAgenda.ConsoleApp.Compartilhado;
using EAgenda.ModuloCompromisso;
using EAgenda.ModuloContato;
using EAgenda.ModuloTarefa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAgenda.Compartilhado
{
    public class TelaMenuPrincipal
    {
        private string opcaoSelecionada;

        private IRepositorio<Contato> repositorioContato;
        private TelaCadastroContato telaCadastroContato;

        private IRepositorio<Compromisso> repositorioCompromisso;
        private TelaCadastroCompromisso telaCadastroCompromisso;

        private IRepositorio<Tarefa> repositorioTarefa;
        private TelaCadastroCompromisso telaCadastroTarefa;

        public TelaMenuPrincipal(Notificador notificador)
        {
            repositorioContato = new RepositorioContato();
            repositorioCompromisso = new RepositorioCompromisso();
            repositorioTarefa = new RepositorioTarefa();

            telaCadastroContato = new TelaCadastroContato(repositorioContato, notificador);
            telaCadastroCompromisso = new TelaCadastroCompromisso(repositorioCompromisso, repositorioContato, telaCadastroContato, notificador);
            telaCadastroTarefa = new TelaCadastroTarefa(repositorioTarefa, notificador);
        }

        public string MostrarOpcoes()
        {
            Console.Clear();

            Console.WriteLine("EAgenda");

            Console.WriteLine();

            Console.WriteLine("Digite 1 para Cadastrar Tarefas");
            Console.WriteLine("Digite 2 para Cadastrar Contatos");
            Console.WriteLine("Digite 3 para Cadastrar Compromissos");

            Console.WriteLine("Digite s para sair");

            opcaoSelecionada = Console.ReadLine();

            return opcaoSelecionada;
        }

        public TelaBase ObterTela()
        {
            string opcao = MostrarOpcoes();

            TelaBase tela = null;

            if (opcao == "1")
                tela = telaCadastroTarefa;

            else if (opcao == "2")
                tela = telaCadastroContato;

            else if (opcao == "3")
                tela = null; //telaCadastroCompromisso;

            return tela;
        }

    }
}
