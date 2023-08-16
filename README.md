## Projeto de Upload para o Vimeo
Projeto funcional para realizar upload de videos para o vimeo.

## Instalação e utilização
#### Front-end
Rodar o comando `npm install` na pasta ClientApp
Rodar o comando `npm start` na pasta ClientApp

#### Back-end
Rodar o comando `dotnet run` na pasta Poc.Vimeo

### Configurando Vimeo
- Acesse o link [Developer Vimeo](https://developer.vimeo.com/apps/) e crie um aplicativo na API do vimeo
- Crie um Personal Access Token com permissão "private" e "upload", para isso, marque a opção "Authenticated (you)"

### Configuração do servidor
No arquivo `appsettings.json` adicione o seguinte código e coloque o token de authenticação
```
"VimeoConfiguration": {
    "AuthenticationToken": "AuthenticationTOkenHEre",
    "Url": "https://api.vimeo.com",
    "UploadPath": "/me/videos"
}
```

## Considerações
O upload é bastante simplificado e fácil a implementação, sendo realizado em dois passos utilizando a abordagem form-based (envio de formulário de arquivo).
- Passo 1: Enviar uma requisição POST para criar o video no vimeo com a seguinte estrutura:
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

    Para verificar todas as opções de privacidade, upload e meta-data do arquivo, consulte [Vimeo Upload](https://developer.vimeo.com/api/reference/videos#upload_video)

- Passo 2: Com o resultado do passo anterior, será gerado um link resultante que deverá ser utilizado para envio do arquivo para o vimeo.
Este link não necessita de headers adicionais para autenticação, pois os parâmetros já fazem este trabalho.

    Com este link, faça uma requisição POST, utilizando um form-data com name `file_data`.

### Possíveis erros por limitação da conta
- Erro Forbiden: Atingido o limite de videos na conta
- Erro Too many Requests: Atingido o limite de requisições por minuto

### Limitações de uso da API
| Plano			    | Quantidade de requisições por minuto (por usuário*) |
| ---               | --- |
| Vimeo Free		| 25 |
| Vimeo Starter     | 125 |
| Vimeo Standard	| 250 |
| Vimeo Advanced	| 750 |
| Vimeo Enterprise  | 2500 |
