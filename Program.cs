namespace CadastroDePessoas
{
class Program
    {
        static void Main(string[] args)
        {
            string opcao;

            List<PessoaFisica> listaPf = new List<PessoaFisica>();

            List<PessoaJuridica> listaPj = new List<PessoaJuridica>();

            barraCarregamento("Iniciando");
            do{
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                Console.WriteLine (@$"
                ===================================================
                |                                                 |
                |       Seja Bem Vindo ao Sistema de Cadastro     |
                |         de Pessoas Físicas e Jurídicas          |
                |_________________________________________________|
                |                                                 |
                |            1 - Cadastrar Pessoa Física          |
                |            2 - Listar Pessoas Físicas           |
                |            3 - Remover uma Pessoa Física        |
                |_________________________________________________|
                |                                                 |
                |            4 - Cadastrar Pessoa Jurídica        |
                |            5 - Listar Pessoas Jurídicas         |
                |            6 - Remover uma Pessoa Jurídica      |
                |                                                 |
                |_________________________________________________|
                |                                                 |
                |             0 - Cancelar/Sair                   |
                |                                                 |
                ===================================================

                ");

                opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                Endereco end1 = new Endereco();

                Console.WriteLine($"Insira o Logradouro:");
                end1.Logradouro = Console.ReadLine();

                Console.WriteLine("Insira o Número do Endereço:");
                end1.Numero = int.Parse(Console.ReadLine());

                Console.WriteLine("Insira o Complemento:");
                end1.Complemento = Console.ReadLine();

                string RespostaComercial;
                bool validado = true;
                do{
                    Console.WriteLine("É um Endereço Comercial? S/N?");
                    RespostaComercial = Console.ReadLine().ToUpper();

                    if(RespostaComercial == "S" || RespostaComercial == "N"){
                        validado = false;
                    }

                }while(validado);

                if(RespostaComercial == "S"){
                    end1.EnderecoComercial = true;
                } else{
                    end1.EnderecoComercial = false;
                }

                PessoaFisica pf = new PessoaFisica();
                pf.endereco = end1;
                Console.WriteLine("Insira o seu Nome:");
                pf.nome = Console.ReadLine();
                Console.WriteLine("Insira o seu CPF:");
                pf.cpf = Console.ReadLine();
                Console.WriteLine("Insira a sua data de nascimento:");
                pf.DataDeNascimento = DateTime.Parse(Console.ReadLine());
            
                bool idadeValida = pf.ValidarDataNascimento(pf.DataDeNascimento);
                if (idadeValida == true){
                Console.WriteLine($"Cadastro Aprovado");
                using(StreamWriter sw = new StreamWriter($"PessoasFísicas.txt", true)){
                    sw.BaseStream.Seek(0, SeekOrigin.End);
                    sw.WriteLine($"Nome: {pf.nome}, CPF:{pf.cpf}, Endereço: {pf.endereco.Logradouro}, {pf.endereco.Numero}, {pf.endereco.Complemento}");
                    Console.WriteLine($"Nome: {pf.nome}, CPF: {pf.cpf}, Data de Nascimento: {pf.DataDeNascimento}");
                    Console.WriteLine($"Endereço: {pf.endereco.Logradouro}, {pf.endereco.Numero}, {pf.endereco.Complemento}");
                }

                }else{
                Console.WriteLine($"Cadastro reprovado");
                }
                Thread.Sleep(3000);

                break;

                case "2":
                using(StreamReader sr = new StreamReader("PessoasFísicas.txt"))
                {
                    string linha;
                    while((linha = sr.ReadLine()) != null){
                        Console.WriteLine($"{linha}");
                    }
                }
                Console.WriteLine($"Pressione Enter para Continuar");
                Console.ReadLine();
                break;

                case "3":
                Console.WriteLine("Digite o CNPJ da Pessoa que deseja Remover");
                string cpf = Console.ReadLine();

                PessoaFisica PFRetornada = listaPf.Find(pf => pf.cpf == cpf);

                if(PFRetornada != null){
                    listaPf.Remove(PFRetornada);
                    Console.WriteLine("Pessoa Removida com Sucesso");
                }else{
                    Console.WriteLine("CPF não encontrado");
                }
                break;

                case "4":

                Endereco end2 = new Endereco();
                Console.WriteLine("Insira o Logradouro:");
                end2.Logradouro = Console.ReadLine();

                Console.WriteLine("Insira o Número:");
                end2.Numero = int.Parse(Console.ReadLine());

                Console.WriteLine("Insira o Complemento:");
                end2.Complemento = Console.ReadLine();

                bool validado2 = true;
                do{
                    Console.WriteLine("É um Endereço Comercial? S/N?");
                    RespostaComercial = Console.ReadLine().ToUpper();

                    if(RespostaComercial == "S" || RespostaComercial == "N"){
                        validado2 = false;
                    }

                }while(validado2);

                PessoaJuridica pj = new PessoaJuridica();
                PessoaJuridica npj = new PessoaJuridica();

                Console.WriteLine("Insira o seu Nome:");
                pj.nome = Console.ReadLine();

                Console.WriteLine("Insira o CNPJ:");
                pj.cnpj = Console.ReadLine();

                pj.endereco = end2;

                Console.WriteLine("Insira a Razão Social da Sua Empresa:");
                pj.RazaoSocial = Console.ReadLine();

                if(npj.ValidarCNPJ(pj.cnpj)){
                Console.WriteLine("CNPJ Valido");
                }else{
                Console.WriteLine("CNPJ Invalido");
                }
                Console.WriteLine($"Nome: {pj.nome}, CNPJ: {pj.cnpj}, Razão Social: {pj.RazaoSocial}");
                Console.WriteLine($"Rua: {pj.endereco.Logradouro}, {pj.endereco.Numero}, {pj.endereco.Complemento}");
                
                using(StreamWriter sw = new StreamWriter($"PessoasJurídicas.txt", true)){
                    sw.BaseStream.Seek(0, SeekOrigin.End);
                    sw.WriteLine($"Nome: {pj.nome}, CPF:{pj.cnpj}, Razão Social: {pj.RazaoSocial} Endereço: {pj.endereco.Logradouro}, {pj.endereco.Numero}, {pj.endereco.Complemento}");
                }

                Thread.Sleep(3000);
                break;

                case "5":
                using(StreamReader sr = new StreamReader("PessoasJurídicas.txt"))
                {
                    string linha;
                    while((linha = sr.ReadLine()) != null){
                        Console.WriteLine($"{linha}");
                    }
                }
                Console.WriteLine($"Pressione Enter para Continuar");
                Console.ReadLine();
                break;

                case "6":

                Console.WriteLine("Digite o CNPJ da Pessoa que deseja Remover");
                string cnpj = Console.ReadLine();

                PessoaJuridica PJLocalizada = listaPj.Find(PJ => PJ.cnpj == cnpj);

                if (PJLocalizada == null){
                    listaPj.Remove(PJLocalizada);
                    Console.WriteLine("Pessoa Removida.");
                } else{
                    Console.WriteLine("CNPJ não encontrado.");
                }
                break;

                case "0":
                Console.WriteLine($"Você Escolheu Sair");
                barraCarregamento("Finalizando");
                Console.ResetColor();
                break;

                default:
                Console.WriteLine($"Opção Inválida");
                break;
            }
        } while (opcao != "0");
        }
    static void barraCarregamento (string TextoCarregamento)
    {
    Console.ResetColor();
    Console.BackgroundColor = ConsoleColor.White;
    Console.ForegroundColor = ConsoleColor.DarkRed;
    Console.Write(TextoCarregamento);
    Thread.Sleep(500);
    for (var contador = 0; contador <10; contador++)
    {
    Console.Write(".");
    Thread.Sleep(500);
    }
    Console.ResetColor(); 
    }

}
}