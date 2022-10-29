# .NET 6.0 Clean Architecture lab
1. [Contextualização](#Contextualização)
2. [Solution Visual Studio](#SolutionVS)
	1. [Rodando o projeto localmente](#RodandoOProjeto)
		1. [Resetando os dados iniciais das feiras (ou carregando novos)](#ImportandoOsDados)



## 1 - Contextualização <a name="Contextualização"></a>
Este projeto é um laboratório de uma *Clean Architecture* usando .NET 6.0, expondo o domínio por meio de uma API. 

O domínio é completamente desacoplado e independente de forma que qualquer interface de apresentação ou infraestrutura (banco de dados) possam ser nele plugados sem necessidade de ajustes e/ou adaptações.
![image](https://user-images.githubusercontent.com/3535044/198698338-ae41014a-cfa6-4699-8df5-1a2657c7f9a1.png)
[*Clean Architecture; horizontal layer view*](https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures)

Pela característica de laboratório, para facilitar a configuração local, optei pelo uso do SQLite para armazenamento das informações de forma a simplificar a execução local do projeto para aqueles que desejarem rodar localmente em seus próprios ambientes.

### 1.1 - API - Feiras Livres (São Paulo)
O *"problema"* hipotético que o projeto visa resolver é a exposição dos dados das [Feiras Livres de São Paulo](https://www.prefeitura.sp.gov.br/cidade/secretarias/subprefeituras/abastecimento/noticias/index.php?p=294187) por meio de uma (ASP.NET) Web API, permitindo o **CRUD das feiras**.
A **pesquisa das feiras** deve ser feita por meio das seguintes informações:
- Distrito
- Região 5
- Nome da feira
- Bairro
> 👆 Essas informações podem ser usada em qualquer combinação na pesquisa, inclusive fazendo a pesquisa usando somente uma delas, duas ou três.

Os dados iniciais usados neste laboratório foram obtidos a partir do arquivo `DEINFO_AB_FEIRASLIVRES_2014.csv` contido [nesse ZIP](http://www.prefeitura.sp.gov.br/cidade/secretarias/upload/chamadas/feiras_livres_1429113213.zip) disponibilizado pela prefeitura de São Paulo. 

## 2 - Solution Visual Studio<a name="SolutionVS"></a>
A IDE usada para criação do projeto foi o Visual Studio 2022.
Essa é a estrutura de projetos e pastas principais:
![estrutura-projeto](https://user-images.githubusercontent.com/3535044/198698547-946bfdd5-5ea4-43de-8da6-580ef8a1b522.png)

### 2.1 - Rodando o projeto localmente<a name="RodandoOProjeto"></a>
Para rodar o projeto, basta executá-lo no Visual Studio. O projeto **FeirasLivres.Api** já vem definido como *Startup Project*, que inicia o projeto web da API abrindo o browser apontando para o Swagger.

⚠ Entretanto, eu **aconselho fortemente** a instalação da instalação da extensão [Rest Client for Visual Studio](https://github.com/madskristensen/RestClientVS) que permite executar e verificar as respostas das requisições REST diretamente do ambiente do Visual Studio, permitindo assim o uso dos arquivos `Distrito.http`, `Feira.http` e `SubPrefeitura.http` que já trazem prontas consultas para teste de todas as operações disponíveis na API.
![rest-client](https://user-images.githubusercontent.com/3535044/198826247-982ffdd7-b3fd-4798-b03d-49e9f50dbdc5.png)

#### 2.1.1 - Resetando os dados iniciais das feiras (ou carregando novos)<a name="ImportandoOsDados"></a>
O projeto já disponibiliza na pasta SqliteDb o arquivo `feiras-livres.db` carregado com as feiras provenientes do arquivo CSV já citado. Entretanto é possível a qualquer momento *"resetar"* as informações do banco de dados com aquelas originalmente contidas no arquivo CSV bastando para isso executar o projeto **ConsoleLab** que, por padrão, apagará todos os dados das tabelas e populará novamente com as informações contidas no arquivo CSV.

![definir-consolelab-como-startup](https://user-images.githubusercontent.com/3535044/198825259-0890b1b6-2c63-4838-b750-46bd0be907d2.png)
