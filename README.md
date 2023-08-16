## Projeto de Upload para o Vimeo
Projeto funcional para realizar upload de videos para o vimeo.

## Instala��o e utiliza��o
#### Front-end
Rodar o comando `npm install` na pasta ClientApp
Rodar o comando `npm start` na pasta ClientApp

#### Back-end
Rodar o comando `dotnet run` na pasta Poc.Vimeo

### Configurando Vimeo
- Acesse o link [Developer Vimeo](https://developer.vimeo.com/apps/) e crie um aplicativo na API do vimeo
- Crie um Personal Access Token com permiss�o "private" e "upload", para isso, marque a op��o "Authenticated (you)"

### Configura��o do servidor
No arquivo `appsettings.json` adicione o seguinte c�digo e coloque o token de authentica��o
```
"VimeoConfiguration": {
    "AuthenticationToken": "AuthenticationTOkenHEre",
    "Url": "https://api.vimeo.com",
    "UploadPath": "/me/videos"
}
```

## Considera��es
O upload � bastante simplificado e f�cil a implementa��o, sendo realizado em dois passos utilizando a abordagem form-based (envio de formul�rio de arquivo).
- Passo 1: Enviar uma requisi��o POST para criar o video no vimeo com a seguinte estrutura:
```
{
    "upload": {
        "size": 1675456,
        "approach": "post"
    },
    "privacy": {
        "view": "nobody"
    },
    "name": "example.mp4"
}
```
Para verificar todas as op��es de privacidade, upload e meta-data do arquivo, consulte 

- Passo 2: Com o resultado do passo anterior, ser� gerado um link resultante que dever� ser utilizado para envio do arquivo para o vimeo.
Este link n�o necessita de headers adicionais para autentica��o, pois os par�metros j� fazem este trabalho.

Com este link, fa�a uma requisi��o POST, utilizando um form-data com name `file_data`.

### Poss�veis erros
- Erro Forbiden: Atingido o limite de videos na conta
- Erro Too many Requests: Atingido o limite de requisi��es por minuto


### Limita��es de uso da API
| Plano			    | Quantidade de requisi��es por minuto (por usu�rio*) |
| ---               | --- |
| Vimeo Free		| 25 |
| Vimeo Starter     | 125 |
| Vimeo Standard	| 250 |
| Vimeo Advanced	| 750 |
| Vimeo Enterprise  | 2500 |
