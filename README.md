## Projeto de Upload para o Vimeo
Projeto funcional para realizar upload de videos para o vimeo.

## Instalação e utilização
#### Front-end
Rodar os comandos `npm install` e `npm start` respectivamente na pasta ClientApp

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
- Passo 1: É enviado uma requisição POST para criar o video no vimeo com a seguinte estrutura:
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
    Essencialmente é necessário somente os dados da propriedade `upload`.
    Essa requisição terá como resultado as informações do Video criado no Vimeo.

    Para verificar todas as opções de privacidade, upload e meta-data do arquivo, consulte [Vimeo Upload](https://developer.vimeo.com/api/reference/videos#upload_video)

- Passo 2: Com o resultado do passo anterior, será gerado um link que é ser utilizado para envio do arquivo de video para o vimeo.
    Este link não necessita de headers adicionais para autenticação, porém necessita ter um Header `Connection: keep-alive`.
    Com este link, uma requisição POST é necessária utilizando um form-data com um `<input type="file" name="file_data>"` (Obs: o name file_data é obrigatório).

Após o upload, o vimeo fará o processamento do video, gerando otimizações de resolução, podendo demorar dependendo do tamanho do video.
Um email é disparado quando o processamento termina, após a confirmação, o video estará pronto para reprodução.

### Possíveis erros por limitação da conta
- Erro Forbiden: Atingido o limite mensal e/ou total de videos da conta.
- Erro Too many Requests: Atingido o limite de requisições por minuto.

### Limitações de uso da API
| Plano			    | Quantidade de requisições por minuto (por usuário*) |
| ---               | --- |
| Vimeo Free		| 25 |
| Vimeo Starter     | 125 |
| Vimeo Standard	| 250 |
| Vimeo Advanced	| 750 |
| Vimeo Enterprise  | 2500 |

A documentação da API especifica os Headers de resposta com a informação de uso da API. Também é especificado algumas formas para reduzir a utilização da mesma.

Essa informação pode ser consultada [neste link](https://developer.vimeo.com/guidelines/rate-limiting)

### Limitações do arquivo enviado
O arquivo de video não pode ultrapassar 250GB e não pode ter duração maior que 24 horas.


### Limitações por conta
Cada plano possui uma quantidade limitada de vídeos armazenado em sua biblioteca, confira os limites [neste link](https://vimeo.com/pt-br/upgrade).

O plano Starter Free possui limite de 25 videos totais e também limite mensal de 2 videos criados / enviados por mês.

#### Referências
https://developer.vimeo.com/api/guides/start

[Método implementado: form-based](https://developer.vimeo.com/api/upload/videos#form-approach)
