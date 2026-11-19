namespace Projeto_Final_API_Usuarios.Constants;

public static class ErrosContants
{
    //Códigos
    public const string COD_USUARIO_NAO_ENCONTRADO = "Usuário não Encontrado";
    public const string COD_EMAIL_CADASTRADO = "Email já cadastrado";
    public const string COD_EMAIL_INVALIDO = "Email inválido.";
    public const string COD_NOME_INVALIDO = "Nome inválido.";
    public const string COD_SENHA_INVALIDA = "Senha inválida.";

    //Mensagens
    public const string MSG_USUARIO_NAO_ENCONTRADO = "Não existe usuario com o Id informado.";
    public const string MSG_EMAIL_CADASTRADO = "Esse e-mail já foi cadastrado anteriormente. Outro e-mail deve ser informado.";
    public const string MSG_EMAIL_INVALIDO = "O email do usuário deve ser informado e deve ter formato válido 'algo@algo.xxx'.";
    public const string MSG_NOME_INVALIDO = "O nome do usuário deve ser informado e deve ter mais do 3 caracteres.";
    public const string MSG_SENHA_INVALIDA = "A senha do usuário deve ser informada e deve ter no mínimo 8 caracteres.";
    
    //OUTRAS CONSTANTES
    public const string STRING_DATA_PADRAO = "dd-MM-yyyy HH:mm:ss";

}