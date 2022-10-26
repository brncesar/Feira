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
| Atributo      | Obrigat�rio | Tipo   | Tamanho | Descri��o
|---------------| :---------: |--------|--------:|-------------
| **nome**      | n�o         | string | 18      | Nome do Distrito Municipal
| **codigo**    | n�o         | string | 9       | C�digo do Distrito Municipal conforme IBGE

> A pesquisa pode ser feita por somente um dos par�metros ou por ambos

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
| Atributo      | Obrigat�rio | Tipo   | Tamanho | Descri��o
|---------------| :---------: |--------|--------:|-------------
| **nome**      | n�o         | string | 25      | Nome da Subprefeitura (31 de 2003 at� 2012)
| **codigo**    | n�o         | string | 2       | C�digo de cada uma das 31 Subprefeituras (2003 a 2012)

>A pesquisa pode ser feita por somente um dos par�metros ou por ambos

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
| Atributo                 | Obrigat�rio | Tipo   | Tamanho | Descri��o
|--------------------------| :---------: |--------|--------:|-------------
| **nome**                 | yes         | string |     30  | Denomina��o da feira livre atribu�da pela Supervis�o de Abastecimento
| **numeroRegistro**       | yes         | string |      6  | N�mero do registro da feira livre na PMSP
| **setorCensitarioIBGE**  | yes         | string |     15  | Setor censit�rio conforme IBGE
| **areaDePonderacaoIBGE** | yes         | string |     13  | �rea de pondera��o (agrupamento de setores censit�rios) conforme IBGE 2010
| **codDistrito**          | yes         | string |      9  | C�digo do Distrito Municipal conforme IBGE
| **codSubPrefeitura**     | yes         | string |      2  | C�digo de cada uma das 31 Subprefeituras (2003 a 2012)
| **regiao5**              | yes         | number |     --- | Regi�o conforme divis�o do Munic�pio em 5 �reas [1: Norte, 2: Leste, 3: Sul, 4: Oeste, 5: Centro]
| **regiao8**              | yes         | number |     --- | Regi�o conforme divis�o do Munic�pio em 8 �reas [11: Norte1, 12: Norte2, 21: Leste1, 22: Leste2, 31: Sul1, 32: Sul2, 4: Oeste, 5: Centro]
| **enderecoLogradouro**   | yes         | string |     34  | Nome do logradouro onde se localiza a feira livre
| **enderecoNumero**       | no          | string |      5  | Um n�mero do logradouro onde se localiza a feira livre
| **enderecoBairro**       | yes         | string |     20  | Bairro de localiza��o da feira livre
| **enderecoReferencia**   | no          | string |     24  | Ponto de refer�ncia da localiza��o da feira livre
| **latitude**             | yes         | number |     --- | N�mero de identifica��o do estabelecimento georreferenciado por SMDU/Deinfo
| **longitude**            | yes         | number |     --- | Longitude da localiza��o do estabelecimento no territ�rio do Munic�pio, conforme MDC


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
| Atributo                 | Obrigat�rio | Tipo   | Tamanho | Descri��o
|--------------------------| :---------: |--------|--------:|-------------
| **nome**                 | n�o         | string |     30  | Denomina��o da feira livre atribu�da pela Supervis�o de Abastecimento
| **numeroRegistro**       | sim         | string |      6  | N�mero do registro da feira livre na PMSP
| **setorCensitarioIBGE**  | n�o         | string |     15  | Setor censit�rio conforme IBGE
| **areaDePonderacaoIBGE** | n�o         | string |     13  | �rea de pondera��o (agrupamento de setores censit�rios) conforme IBGE 2010
| **codDistrito**          | n�o         | string |      9  | C�digo do Distrito Municipal conforme IBGE
| **codSubPrefeitura**     | n�o         | string |      2  | C�digo de cada uma das 31 Subprefeituras (2003 a 2012)
| **regiao5**              | n�o         | number |     --- | Regi�o conforme divis�o do Munic�pio em 5 �reas [1: Norte, 2: Leste, 3: Sul, 4: Oeste, 5: Centro]
| **regiao8**              | n�o         | number |     --- | Regi�o conforme divis�o do Munic�pio em 8 �reas [11: Norte1, 12: Norte2, 21: Leste1, 22: Leste2, 31: Sul1, 32: Sul2, 4: Oeste, 5: Centro]
| **enderecoLogradouro**   | n�o         | string |     34  | Nome do logradouro onde se localiza a feira livre
| **enderecoNumero**       | n�o         | string |      5  | Um n�mero do logradouro onde se localiza a feira livre
| **enderecoBairro**       | n�o         | string |     20  | Bairro de localiza��o da feira livre
| **enderecoReferencia**   | n�o         | string |     24  | Ponto de refer�ncia da localiza��o da feira livre
| **latitude**             | n�o         | number |     --- | N�mero de identifica��o do estabelecimento georreferenciado por SMDU/Deinfo
| **longitude**            | n�o         | number |     --- | Longitude da localiza��o do estabelecimento no territ�rio do Munic�pio, conforme MDC

> A feira a ser atualizada ser� aquela referenciada pelo n�mero de registro
>
> Somente os par�metros passados ser�o atualizados

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
| Atributo           | Obrigat�rio | Tipo   | Tamanho | Descri��o
|--------------------| :---------: |--------|--------:|-------------
| **nome**           | n�o         | string |     30  | Denomina��o da feira livre atribu�da pela Supervis�o de Abastecimento
| **codDistrito**    | n�o         | string |      9  | C�digo do Distrito Municipal conforme IBGE2003 a 2012)
| **regiao5**        | n�o         | number |     --- | Regi�o conforme divis�o do Munic�pio em 5  feira livre
| **enderecoBairro** | n�o         | string |     20  | Bairro de localiza��o da feira livre

> A pesquisa pode ser feita utilizando qualquer combina��o dos par�metros

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
