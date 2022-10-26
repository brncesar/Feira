# API Feiras Livres

- [API Feiras Livres](#api-feiras-livres)
    - [Distrito](#distrito)
    	- [Find endpoint](#find-distrito-endpoint)
    		- [Request parameters](#find-distrito-request-parameters)
			- [Server response](#find-distrito-server-response)
    - [SubPrefeitura](#subprefeitura)
    	- [Find endpoint](#find-subprefeitura-endpoint)
    		- [Request parameters](#find-subprefeitura-request-parameters)
			- [Server response](#find-subprefeitura-server-response)
    - [Feira](#feira)
    	- [Add endpoint](#add-feira-endpoint)
    		- [Request parameters](#add-feira-request-parameters)
			- [Server response](#add-feira-server-response)
    	- [Edit endpoint](#edit-feira-endpoint)
    		- [Request parameters](#edit-feira-request-parameters)
			- [Server response](#edit-feira-server-response)
    	- [Find endpoint](#find-feira-endpoint)
    		- [Request parameters](#find-feira-request-parameters)
			- [Server response](#find-feira-server-response)
    	- [Remove endpoint](#remove-feira-endpoint)
    		- [Request parameters](#remove-feira-request-parameters)
			- [Server response](#remove-feira-server-response)



## Distrito

### Find Distrito - endpoint
```json
GET {{host}}/distrito/find
```

#### Find Distrito - request parameters
```json
{
	"nome"   : "CAMPO"
	"codigo" : "16"
}
```
| Atributo      | Obrigatório | Tipo   | Tamanho | Descrição
|---------------| :---------: |--------|--------:|-------------
| **nome**      | não         | string | 18      | Nome do Distrito Municipal
| **codigo**    | não         | string | 9       | Código do Distrito Municipal conforme IBGE

> A pesquisa pode ser feita por somente um dos parâmetros ou por ambos

#### Find Distrito - server response
```json
[
	{
		"nome"   : "CAMPO GRANDE",
		"codigo" : "16"
	}
]
```
> Retorna um array com os itens encontrados. Se nenhum item for encontrado, retorna um array vazio.

---


## SubPrefeitura

### Find SubPrefeitura - endpoint
```json
GET {{host}}/subprefeitura/find
```

#### Find SubPrefeitura - request parameters
```json
{
	"nome"   : "FORMOSA",
	"codigo" : "26"
}
```
| Atributo      | Obrigatório | Tipo   | Tamanho | Descrição
|---------------| :---------: |--------|--------:|-------------
| **nome**      | não         | string | 25      | Nome da Subprefeitura (31 de 2003 até 2012)
| **codigo**    | não         | string | 2       | Código de cada uma das 31 Subprefeituras (2003 a 2012)

>A pesquisa pode ser feita por somente um dos parâmetros ou por ambos

#### Find SubPrefeitura - server response
```json
[
	{
		"nome"   : "ARICANDUVA-FORMOSA-CARRAO",
		"codigo" : "26"
	}
]
```
> Retorna um array com os itens encontrados. Se nenhum item for encontrado, retorna um array vazio.


---


## Feira

### Add Feira - endpoint
```json
POST {{host}}/feira/add
```

#### Add Feira - request parameters
```json
{
	"nome"                 : "PIRASSUNUNGA",
	"numeroRegistro"       : "1013-8",
	"setorCensitarioIBGE"  : "355030801000054",
	"areaDePonderacaoIBGE" : "3550308005039",
	"codDistrito"          : "01",
	"codSubPrefeitura"     : "25",
	"regiao5"              : 2,
	"regiao8"              : 21,
	"enderecoLogradouro"   : "RUA TEREZINA",
	"enderecoNumero"       : "615",
	"enderecoBairro"       : "ALTO DA MOOCA",
	"enderecoReferencia"   : "CAMPO LARGO E MANAUS",
	"latitude"             : -23.564711,
	"longitude"            : -46.564711
}
```
| Atributo                 | Obrigatório | Tipo   | Tamanho | Descrição
|--------------------------| :---------: |--------|--------:|-------------
| **nome**                 | yes         | string |     30  | Denominação da feira livre atribuída pela Supervisão de Abastecimento
| **numeroRegistro**       | yes         | string |      6  | Número do registro da feira livre na PMSP
| **setorCensitarioIBGE**  | yes         | string |     15  | Setor censitário conforme IBGE
| **areaDePonderacaoIBGE** | yes         | string |     13  | Área de ponderação (agrupamento de setores censitários) conforme IBGE 2010
| **codDistrito**          | yes         | string |      9  | Código do Distrito Municipal conforme IBGE
| **codSubPrefeitura**     | yes         | string |      2  | Código de cada uma das 31 Subprefeituras (2003 a 2012)
| **regiao5**              | yes         | number |     --- | Região conforme divisão do Município em 5 áreas [1: Norte, 2: Leste, 3: Sul, 4: Oeste, 5: Centro]
| **regiao8**              | yes         | number |     --- | Região conforme divisão do Município em 8 áreas [11: Norte1, 12: Norte2, 21: Leste1, 22: Leste2, 31: Sul1, 32: Sul2, 4: Oeste, 5: Centro]
| **enderecoLogradouro**   | yes         | string |     34  | Nome do logradouro onde se localiza a feira livre
| **enderecoNumero**       | no          | string |      5  | Um número do logradouro onde se localiza a feira livre
| **enderecoBairro**       | yes         | string |     20  | Bairro de localização da feira livre
| **enderecoReferencia**   | no          | string |     24  | Ponto de referência da localização da feira livre
| **latitude**             | yes         | number |     --- | Número de identificação do estabelecimento georreferenciado por SMDU/Deinfo
| **longitude**            | yes         | number |     --- | Longitude da localização do estabelecimento no território do Município, conforme MDC


#### Add Feira - server response
```json
{
	"id" : "fa7e6176-cf00-4656-9779-ac4567c6845b"
}
```



### Edit Feira - endpoint
```json
PUT {{host}}/feira/edit
```

#### Edit Feira - request parameters
```json
{
	"nome"                 : "PIRASSUNUNGA",
	"numeroRegistro"       : "1013-8",
	"setorCensitarioIBGE"  : "355030801000054",
	"areaDePonderacaoIBGE" : "3550308005039",
	"codDistrito"          : "01",
	"codSubPrefeitura"     : "25",
	"regiao5"              : 2,
	"regiao8"              : 21,
	"enderecoLogradouro"   : "RUA TEREZINA",
	"enderecoNumero"       : "615",
	"enderecoBairro"       : "ALTO DA MOOCA",
	"enderecoReferencia"   : "CAMPO LARGO E MANAUS",
	"latitude"             : -23.564711,
	"longitude"            : -46.564711
}
```
| Atributo                 | Obrigatório | Tipo   | Tamanho | Descrição
|--------------------------| :---------: |--------|--------:|-------------
| **nome**                 | não         | string |     30  | Denominação da feira livre atribuída pela Supervisão de Abastecimento
| **numeroRegistro**       | sim         | string |      6  | Número do registro da feira livre na PMSP
| **setorCensitarioIBGE**  | não         | string |     15  | Setor censitário conforme IBGE
| **areaDePonderacaoIBGE** | não         | string |     13  | Área de ponderação (agrupamento de setores censitários) conforme IBGE 2010
| **codDistrito**          | não         | string |      9  | Código do Distrito Municipal conforme IBGE
| **codSubPrefeitura**     | não         | string |      2  | Código de cada uma das 31 Subprefeituras (2003 a 2012)
| **regiao5**              | não         | number |     --- | Região conforme divisão do Município em 5 áreas [1: Norte, 2: Leste, 3: Sul, 4: Oeste, 5: Centro]
| **regiao8**              | não         | number |     --- | Região conforme divisão do Município em 8 áreas [11: Norte1, 12: Norte2, 21: Leste1, 22: Leste2, 31: Sul1, 32: Sul2, 4: Oeste, 5: Centro]
| **enderecoLogradouro**   | não         | string |     34  | Nome do logradouro onde se localiza a feira livre
| **enderecoNumero**       | não         | string |      5  | Um número do logradouro onde se localiza a feira livre
| **enderecoBairro**       | não         | string |     20  | Bairro de localização da feira livre
| **enderecoReferencia**   | não         | string |     24  | Ponto de referência da localização da feira livre
| **latitude**             | não         | number |     --- | Número de identificação do estabelecimento georreferenciado por SMDU/Deinfo
| **longitude**            | não         | number |     --- | Longitude da localização do estabelecimento no território do Município, conforme MDC

> A feira a ser atualizada será aquela referenciada pelo número de registro
>
> Somente os parâmetros passados serão atualizados

#### Edit Feira - server response
```json
true
```



### Find Feira - endpoint
```json
GET {{host}}/feira/find
```

#### Find Feira - request parameters
```json
{
	"nome"        : "PIRA",
	"codDistrito" : "01",
	"regiao5"     : 2,
	"bairro"      : "MOOCA",
}
```
| Atributo           | Obrigatório | Tipo   | Tamanho | Descrição
|--------------------| :---------: |--------|--------:|-------------
| **nome**           | não         | string |     30  | Denominação da feira livre atribuída pela Supervisão de Abastecimento
| **codDistrito**    | não         | string |      9  | Código do Distrito Municipal conforme IBGE2003 a 2012)
| **regiao5**        | não         | number |     --- | Região conforme divisão do Município em 5  feira livre
| **enderecoBairro** | não         | string |     20  | Bairro de localização da feira livre

> A pesquisa pode ser feita utilizando qualquer combinação dos parâmetros

#### Find Feira - server response
```json
[
	{
		"nome"                 : "PIRASSUNUNGA",
		"numeroRegistro"       : "1013-8",
		"setorCensitarioIBGE"  : "355030801000054",
		"areaDePonderacaoIBGE" : "3550308005039",
		"codDistrito"          : "01",
		"Distrito"             : "AGUA RASA",
		"codSubPrefeitura"     : "25",
		"SubPrefeitura"        : "MOOCA",
		"regiao5"              : 2,
		"regiao8"              : 21,
		"enderecoLogradouro"   : "RUA TEREZINA",
		"enderecoNumero"       : "615",
		"enderecoBairro"       : "ALTO DA MOOCA",
		"enderecoReferencia"   : "CAMPO LARGO E MANAUS",
		"latitude"             : -23.564711,
		"longitude"            : -46.564711
	}
]
```
> Retorna um array com os itens encontrados. Se nenhum item for encontrado, retorna um array vazio.



### Remove Feira - endpoint
```json
DELETE {{host}}/feira/remove
```

#### Remove Feira - request parameters
```json
{
	"numeroRegistro": "1013-8"
}
```

#### Remove Feira - server response
```json
true
```


---
