using EAgenda.Compartilhado;
using EAgenda.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAgenda.ModuloContato
{
    public class TelaCadastroContato : TelaBase, ITelaCadastravel
    {
        private readonly Notificador notificador;
        private readonly IRepositorio<Contato> repositorioContato;

        public TelaCadastroContato(IRepositorio<Contato> repositorioContato, Notificador notificador)
            : base("Cadastro de Contatos")
        {   
            this.repositorioContato = repositorioContato;
            this.notificador = notificador;
        }

        public override string MostrarOpcoes()
        {
            MostrarTitulo(Titulo);

            Console.WriteLine("Digite 1 para Inserir");
            Console.WriteLine("Digite 2 para Editar");
            Console.WriteLine("Digite 3 para Excluir");
            Console.WriteLine("Digite 4 para Visualizar");

            Console.WriteLine("Digite s para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }


        public void InserirRegistro()
        {
            MostrarTitulo("Inserindo novo contato");

            Contato novoContato = ObterContato();

            string statusValidacao = repositorioContato.Inserir(novoContato);

            if (statusValidacao == "REGISTRO_VALIDO")
                notificador.ApresentarMensagem("Contato cadastrado com sucesso!", TipoMensagem.Sucesso);
            else
                notificador.ApresentarMensagem(statusValidacao, TipoMensagem.Erro);
        }

        public void EditarRegistro()
        {
            MostrarTitulo("Editando Contato");

            bool temContatosCadastrados = VisualizarRegistros("Pesquisando");

            if (temContatosCadastrados == false)
            {
                notificador.ApresentarMensagem("Nenhum contato cadastrado para poder editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroContato = ObterNumeroContato();

            Contato contatoAtualizado = ObterContato();

            bool conseguiuEditar = repositorioContato.Editar(x => x.numero == numeroContato, contatoAtualizado);

            if (!conseguiuEditar)
                notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Contato editado com sucesso", TipoMensagem.Sucesso);
        }

        public void ExcluirRegistro()
        {
            MostrarTitulo("Excluindo Contato");

            bool temContatosCadastrados = VisualizarRegistros("Pesquisando");

            if (temContatosCadastrados == false)
            {
                notificador.ApresentarMensagem(
                    "Nenhum contato cadastrado para poder excluir", TipoMensagem.Atencao);
                return;
            }

            int numeroContato = ObterNumeroContato();

            bool conseguiuExcluir = repositorioContato.Excluir(x => x.numero == numeroContato);

            if (!conseguiuExcluir)
                notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Sucesso);
            else
                notificador.ApresentarMensagem("Contato excluído com sucesso!", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Contatos");

            List<Contato> contatos = repositorioContato.SelecionarTodos();

            if (contatos.Count == 0)
            {
                notificador.ApresentarMensagem("Não há nenhum contato disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Contato contato in contatos)
                Console.WriteLine(contato.ToString());

            Console.ReadLine();

            return true;
        }

        #region Métodos privados
        private int ObterNumeroContato()
        {
            int numeroContato;
            bool numeroContatoEncontrado;

            do
            {
                Console.Write("Digite o número do contato que deseja selecionar: ");
                numeroContato = Convert.ToInt32(Console.ReadLine());

                numeroContatoEncontrado = repositorioContato.ExisteRegistro(x => x.numero == numeroContato);

                if (numeroContatoEncontrado == false)
                    notificador.ApresentarMensagem("Número do contato não encontrado, digite novamente.", TipoMensagem.Atencao);

            } while (numeroContatoEncontrado == false);
            return numeroContato;
        }

        private Contato ObterContato()
        {
            Console.Write("Digite o nome do contato: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o e-mail: ");
            string email = Console.ReadLine();

            Console.Write("Digite o número do telefone: ");
            string telefone = Console.ReadLine();

            Console.Write("Digite o endereço: ");
            string endereco = Console.ReadLine();

            Console.Write("Digite o cargo: ");
            string cargo = Console.ReadLine();

            Contato contato = new(nome, email, telefone, endereco, cargo);

            return contato;
        }
        #endregion
    }
}
