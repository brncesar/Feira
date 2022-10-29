# .NET 6.0 Clean Architecture lab
1. [Contextualização](#Contextualização)
2. [Solution Visual Studio](#SolutionVS)
	1. [Rodando o projeto localmente](#RodandoOProjeto)
		1. [Resetando os dados iniciais das feiras (ou carregando novos)](#ImportandoOsDados)
3. [Domain](#Domain)
	1.	[Entidades/Casos de uso/DTO's](#EntidadesCasosDeUsoDtos)
4. [API Endpoints](#ApiEndpoints)
5. [Log's](#Logs)
6. [Testes](#Testes)



## 1 - Contextualização <a name="Contextualização"></a>
Este projeto é um laboratório de uma *Clean Architecture* usando .NET 6.0, expondo o domínio por meio de uma API. 

O domínio é completamente desacoplado e independente de forma que qualquer interface de apresentação ou infraestrutura (banco de dados) possam ser nele plugados sem necessidade de ajustes e/ou adaptações.
![clean-arch](https://user-images.githubusercontent.com/3535044/198844956-253c6b5d-06c1-48d2-80fb-c8504d6f2b4c.png)

> *Fonte da imagem: [*Clean Architecture; horizontal layer view*](https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures)*

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

## 3 - Domain<a name="Domain"></a>
Visando a completa abstração do mundo externo a partir da perspectiva do domínio, a interface `IDomainActionResult` é a responsável por padronizar a forma como o mundo externo responde as requisições do domínio, também sendo usada pelo domínio para responder solicitações ao mundo externo.
Na definição de seus métodos de negócio 
![comunicacao](https://user-images.githubusercontent.com/3535044/198842361-07492d31-7f63-4e68-9e36-c2bd69869b37.png)

### 3.1 - ![entity!](https://p.yusukekamiyamane.com/icons/search/fugue/icons/document-list.png)  Entidades / ![useCase!](https://p.yusukekamiyamane.com/icons/search/fugue/icons/lightning.png) Casos de uso / ![dto!](https://p.yusukekamiyamane.com/icons/search/fugue/icons/envelope-share.png) DTO's<a name="EntidadesCasosDeUsoDtos"></a>
- ![entity!](https://p.yusukekamiyamane.com/icons/search/fugue/icons/document-list.png) [Distrito](https://github.com/brncesar/Feira/blob/master/FeirasLivres.Domain/Entities/DistritoEntity/Distrito.cs)
	- ![useCase!](https://p.yusukekamiyamane.com/icons/search/fugue/icons/lightning.png) [FindDistrito](https://github.com/brncesar/Feira/blob/master/FeirasLivres.Domain/Entities/DistritoEntity/FindDistritoUseCase/FindDistrito.cs)
		- ![dto!](https://p.yusukekamiyamane.com/icons/search/fugue/icons/envelope-share.png) [FindDistritoParams](https://github.com/brncesar/Feira/blob/master/FeirasLivres.Domain/Entities/DistritoEntity/FindDistritoUseCase/FindDistrito.Params.cs)
		- ![dto!](https://p.yusukekamiyamane.com/icons/search/fugue/icons/envelope-share.png) [FindDistritoResult](https://github.com/brncesar/Feira/blob/master/FeirasLivres.Domain/Entities/DistritoEntity/FindDistritoUseCase/FindDistrito.Result.cs)
	- ![useCase!](https://p.yusukekamiyamane.com/icons/search/fugue/icons/lightning.png) [GetDistritoByCode](https://github.com/brncesar/Feira/blob/master/FeirasLivres.Domain/Entities/DistritoEntity/GetDistritoByCodigoUseCase/GetDistritoByCodigo.cs)
		- ![dto!](https://p.yusukekamiyamane.com/icons/search/fugue/icons/envelope-share.png) [GetDistritoByCodeParam](https://github.com/brncesar/Feira/blob/master/FeirasLivres.Domain/Entities/DistritoEntity/GetDistritoByCodigoUseCase/GetDistritoByCodigo.Params.cs)
		- ![dto!](https://p.yusukekamiyamane.com/icons/search/fugue/icons/envelope-share.png) [GetDistritoByCodeResult](https://github.com/brncesar/Feira/blob/master/FeirasLivres.Domain/Entities/DistritoEntity/GetDistritoByCodigoUseCase/GetDistritoByCodigo.Result.cs)
- ![entity!](https://p.yusukekamiyamane.com/icons/search/fugue/icons/document-list.png) [SubPrefeitura](https://github.com/brncesar/Feira/blob/master/FeirasLivres.Domain/Entities/SubPrefeituraEntity/SubPrefeitura.cs)
	- ![useCase!](https://p.yusukekamiyamane.com/icons/search/fugue/icons/lightning.png) [FindSubPrefeitura](https://github.com/brncesar/Feira/blob/master/FeirasLivres.Domain/Entities/SubPrefeituraEntity/FindSubPrefeituraUseCase/FindSubPrefeitura.cs)
		- ![dto!](https://p.yusukekamiyamane.com/icons/search/fugue/icons/envelope-share.png) [FindSubPrefeituraParams](https://github.com/brncesar/Feira/blob/master/FeirasLivres.Domain/Entities/SubPrefeituraEntity/FindSubPrefeituraUseCase/FindSubPrefeitura.Params.cs)
		- ![dto!](https://p.yusukekamiyamane.com/icons/search/fugue/icons/envelope-share.png) [FindSubPrefeituraResult](https://github.com/brncesar/Feira/blob/master/FeirasLivres.Domain/Entities/SubPrefeituraEntity/FindSubPrefeituraUseCase/FindSubPrefeitura.Result.cs)
	- ![useCase!](https://p.yusukekamiyamane.com/icons/search/fugue/icons/lightning.png) [GetSubPrefeituraByCodigo](https://github.com/brncesar/Feira/blob/master/FeirasLivres.Domain/Entities/SubPrefeituraEntity/GetSubPrefeituraByCodigoUseCase/GetSubPrefeituraByCodigo.cs)
		- ![dto!](https://p.yusukekamiyamane.com/icons/search/fugue/icons/envelope-share.png) [GetSubPrefeituraByCodigoParam](https://github.com/brncesar/Feira/blob/master/FeirasLivres.Domain/Entities/SubPrefeituraEntity/GetSubPrefeituraByCodigoUseCase/GetSubPrefeituraByCodigo.Params.cs)
		- ![dto!](https://p.yusukekamiyamane.com/icons/search/fugue/icons/envelope-share.png) [GetSubPrefeituraByCodigoResult](https://github.com/brncesar/Feira/blob/master/FeirasLivres.Domain/Entities/SubPrefeituraEntity/GetSubPrefeituraByCodigoUseCase/GetSubPrefeituraByCodigo.Result.cs)
- ![entity!](https://p.yusukekamiyamane.com/icons/search/fugue/icons/document-list.png) [Feira](https://github.com/brncesar/Feira/blob/master/FeirasLivres.Domain/Entities/FeiraEntity/Feira.cs)
	- ![useCase!](https://p.yusukekamiyamane.com/icons/search/fugue/icons/lightning.png) [AddNewFeira](https://github.com/brncesar/Feira/blob/master/FeirasLivres.Domain/Entities/FeiraEntity/AddNewFeiraUseCase/AddNewFeira.cs)
		- ![dto!](https://p.yusukekamiyamane.com/icons/search/fugue/icons/envelope-share.png) [AddNewFeiraParams](https://github.com/brncesar/Feira/blob/master/FeirasLivres.Domain/Entities/FeiraEntity/AddNewFeiraUseCase/AddNewFeira.Params.cs)
		- ![dto!](https://p.yusukekamiyamane.com/icons/search/fugue/icons/envelope-share.png) [FeiraResult](https://github.com/brncesar/Feira/blob/master/FeirasLivres.Domain/Entities/FeiraEntity/_Common/FeiraResult.cs)
	- ![useCase!](https://p.yusukekamiyamane.com/icons/search/fugue/icons/lightning.png) [EditExisting](https://github.com/brncesar/Feira/blob/master/FeirasLivres.Domain/Entities/FeiraEntity/EditExistingFeiraUseCase/EditExistingFeira.cs)
		- ![dto!](https://p.yusukekamiyamane.com/icons/search/fugue/icons/envelope-share.png) [EditExistingParams](https://github.com/brncesar/Feira/blob/master/FeirasLivres.Domain/Entities/FeiraEntity/EditExistingFeiraUseCase/EditExistingFeira.Params.cs)
		- ![dto!](https://p.yusukekamiyamane.com/icons/search/fugue/icons/envelope-share.png) [FeiraResult](https://github.com/brncesar/Feira/blob/master/FeirasLivres.Domain/Entities/FeiraEntity/_Common/FeiraResult.cs)
	- ![useCase!](https://p.yusukekamiyamane.com/icons/search/fugue/icons/lightning.png) [FindFeira](https://github.com/brncesar/Feira/blob/master/FeirasLivres.Domain/Entities/FeiraEntity/FindFeiraUseCase/FindFeira.cs)
		- ![dto!](https://p.yusukekamiyamane.com/icons/search/fugue/icons/envelope-share.png) [FindFeiraParams](https://github.com/brncesar/Feira/blob/master/FeirasLivres.Domain/Entities/FeiraEntity/FindFeiraUseCase/FindFeira.Params.cs)
		- ![dto!](https://p.yusukekamiyamane.com/icons/search/fugue/icons/envelope-share.png) [FeiraResult](https://github.com/brncesar/Feira/blob/master/FeirasLivres.Domain/Entities/FeiraEntity/_Common/FeiraResult.cs)
	- ![useCase!](https://p.yusukekamiyamane.com/icons/search/fugue/icons/lightning.png) [RemoveExistingFeira](https://github.com/brncesar/Feira/blob/master/FeirasLivres.Domain/Entities/FeiraEntity/RemoveExistingFeiraUseCase/RemoveExistingFeira.cs)
		- ![dto!](https://p.yusukekamiyamane.com/icons/search/fugue/icons/envelope-share.png) [RemoveExistingFeiraParams](https://github.com/brncesar/Feira/blob/master/FeirasLivres.Domain/Entities/FeiraEntity/RemoveExistingFeiraUseCase/RemoveExistingFeira.Params.cs)


## 4 - Endpoints<a name="EndPoints"></a>
A API deste projeto fornece métodos de consulta e recuperação de Distritos e Sub-Prefeituras por nome e código, além de permitir consultar, edita, adicionar e excluir Feiras.
A documentação detalhada de cada um dos endpoints pode ser encontrada no arquivo [Api.md](https://github.com/brncesar/Feira/blob/master/Docs/Api.md) na pasta **Docs** deste projeto.

## 5 - Logs<a name="Logs"></a>

## 6 - Testes<a name="Testes"></a>
Para executar os testes basta clicar com o botão direito do mouse no projeto **FeirasLivres.Domain.Test**, depois na opção *"Open in Terminal"*, escrever o nome do ShellScript `generate-test-cover-report.ps1` no terminal e apertar a tecla ENTER para executá-lo.

![executar-os-testes](https://user-images.githubusercontent.com/3535044/198849189-f503b066-f441-485a-8345-273a5bb7066a.png)

A execução desse ShellScript irá:
1. Executar os testes que ao final geram o arquivo xml contendo os resultados analíticos.
2. Gerar o relatório de abrangência de cobertura dos testes
3. Abrir no browser a página do relatório
