using EAgenda.Compartilhado;
using EAgenda.ConsoleApp.Compartilhado;
using EAgenda.ModuloCompromisso;
using EAgenda.ModuloContato;
using System;

namespace EAgenda
{
    internal class Program
    {
        static Notificador notificador = new Notificador();
        static TelaMenuPrincipal menuPrincipal = new TelaMenuPrincipal(notificador);

        static void Main(string[] args)
        {
            Console.Title = "### EAGENDA ###";

            while (true)
            {
                TelaBase telaSelecionada = menuPrincipal.ObterTela();

                if (telaSelecionada is null)
                    return;

                string opcaoSelecionada = telaSelecionada.MostrarOpcoes();

                if (telaSelecionada is ITelaCadastravel)
                    GerenciarCadastroBasico(telaSelecionada, opcaoSelecionada);

                else if (telaSelecionada is TelaCadastroCompromisso)
                    GerenciarCadastroCompromissos(telaSelecionada, opcaoSelecionada);
            }
        }

        private static void GerenciarCadastroBasico(TelaBase telaSelecionada, string opcaoSelecionada)
        {
            ITelaCadastravel telaCadastroBasico = telaSelecionada as ITelaCadastravel;

            if (telaCadastroBasico is null)
                return;

            if (opcaoSelecionada == "1")
                telaCadastroBasico.InserirRegistro();

            else if (opcaoSelecionada == "2")
                telaCadastroBasico.EditarRegistro();

            else if (opcaoSelecionada == "3")
                telaCadastroBasico.ExcluirRegistro();

            else if (opcaoSelecionada == "4")
                telaCadastroBasico.VisualizarRegistros("Tela");

            TelaCadastroContato telaCadastroContato = telaCadastroBasico as TelaCadastroContato;

            if (telaCadastroContato is null)
                return;
        }
        private static void GerenciarCadastroCompromissos(TelaBase telaSelecionada, string opcaoSelecionada)
        {
            TelaCadastroCompromisso telaCadastroCompromisso = telaSelecionada as TelaCadastroCompromisso;

            if (telaCadastroCompromisso is null)
                return;

            if (opcaoSelecionada == "1")
                telaCadastroCompromisso.RegistrarCompromisso();

            else if (opcaoSelecionada == "2")
                telaCadastroCompromisso.EditarCompromisso();

            else if (opcaoSelecionada == "3")
                telaCadastroCompromisso.ExcluirCompromisso();

            else if (opcaoSelecionada == "4")
                telaCadastroCompromisso.VisualizarCompromissos("Tela");

            else if (opcaoSelecionada == "5")
                telaCadastroCompromisso.VisualizarCompromissosEmAberto("Tela");
        }
    }
}
