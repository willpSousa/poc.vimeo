## Projeto de Upload para o Vimeo
Projeto funcional para realizar upload de videos para o vimeo.

## Instala��o e utiliza��o
#### Front-end
Rodar os comandos `npm install` e `npm start` respectivamente na pasta ClientApp

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
- Passo 1: � enviado uma requisi��o POST para criar o video no vimeo com a seguinte estrutura:
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
    Essencialmente � necess�rio somente os dados da propriedade `upload`.
    Essa requisi��o ter� como resultado as informa��es do Video criado no Vimeo.

    Para verificar todas as op��es de privacidade, upload e meta-data do arquivo, consulte [Vimeo Upload](https://developer.vimeo.com/api/reference/videos#upload_video)

- Passo 2: Com o resultado do passo anterior, ser� gerado um link que � ser utilizado para envio do arquivo de video para o vimeo.
    Este link n�o necessita de headers adicionais para autentica��o, por�m necessita ter um Header `Connection: keep-alive`.
    Com este link, uma requisi��o POST � necess�ria utilizando um form-data com um `<input type="file" name="file_data>"` (Obs: o name file_data � obrigat�rio).

Ap�s o upload, o vimeo far� o processamento do video, gerando otimiza��es de resolu��o, podendo demorar dependendo do tamanho do video.
Um email � disparado quando o processamento termina, ap�s a confirma��o, o video estar� pronto para reprodu��o.

### Poss�veis erros por limita��o da conta
- Erro Forbiden: Atingido o limite mensal e/ou total de videos da conta.
- Erro Too many Requests: Atingido o limite de requisi��es por minuto.

### Limita��es de uso da API
| Plano			    | Quantidade de requisi��es por minuto (por usu�rio*) |
| ---               | --- |
| Vimeo Free		| 25 |
| Vimeo Starter     | 125 |
| Vimeo Standard	| 250 |
| Vimeo Advanced	| 750 |
| Vimeo Enterprise  | 2500 |

A documenta��o da API especifica os Headers de resposta com a informa��o de uso da API. Tamb�m � especificado algumas formas para reduzir a utiliza��o da mesma.

Essa informa��o pode ser consultada [neste link](https://developer.vimeo.com/guidelines/rate-limiting)

### Limita��es do arquivo enviado
O arquivo de video n�o pode ultrapassar 250GB e n�o pode ter dura��o maior que 24 horas.


### Limita��es por conta
Cada plano possui uma quantidade limitada de v�deos armazenado em sua biblioteca, confira os limites [neste link](https://vimeo.com/pt-br/upgrade).

O plano Starter Free possui limite de 25 videos totais e tamb�m limite mensal de 2 videos criados / enviados por m�s.

#### Refer�ncias
https://developer.vimeo.com/api/guides/start

[M�todo implementado: form-based](https://developer.vimeo.com/api/upload/videos#form-approach)
