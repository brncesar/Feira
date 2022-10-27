# API Feiras Livres

- [API Feiras Livres](#api-feiras-livres)
    - [Distrito](#distrito)
    	- [Find endpoint](#distrito-find-endpoint)
    		- [Request parameters](#distrito-find-request-parameters)
			- [Server response](#distrito-find-server-response)
    	- [GetByCodigo endpoint](#distrito-getbycodigo-endpoint)
    		- [Request parameters](#distrito-getbycodigo-request-parameters)
			- [Server response](#distrito-getbycodigo-server-response)
    - [SubPrefeitura](#subprefeitura)
    	- [Find endpoint](#subprefeitura-find-endpoint)
    		- [Request parameters](#subprefeitura-find-request-parameters)
			- [Server response](#subprefeitura-find-server-response)
    	- [GetByCodigo endpoint](#subprefeitura-getbycodigo-endpoint)
    		- [Request parameters](#subprefeitura-getbycodigo-request-parameters)
			- [Server response](#subprefeitura-getbycodigo-server-response)
    - [Feira](#feira)
    	- [Find endpoint](#feira-find-endpoint)
    		- [Request parameters](#feira-find-request-parameters)
			- [Server response](#feira-find-server-response)
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

### Distrito: Find - endpoint
```json
GET {{host}}/api/distrito/find?nome={{nameToFind}}&codigo={{codToFind}}
```

#### Distrito: Find - request parameters
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

#### Distrito: Find - server response
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

### Distrito: GetByCodigo - endpoint
```json
GET {{host}}/api/distrito/GetByCodigo/{{codDistrito}}
```

#### Distrito: GetByCodigo - request parameters
```json
{
	"codDistrito" : "16"
}
```
| Atributo        | Obrigatório | Tipo   | Tamanho | Descrição
|-----------------| :---------: |--------|--------:|-------------
| **codDistrito** | sim         | string | 9       | Código do Distrito Municipal conforme IBGE

> O atributo não tem nome. Deve ser passado o valor direamente na chamada da URL

#### Distrito: GetByCodigo - server response
```json
{
	"nome"   : "CAMPO GRANDE",
	"codigo" : "16"
}
```
> Retorna o item encontrado. Se nenhum item for encontrado, retorna 404.

---


## SubPrefeitura

### SubPrefeitura: Find - endpoint
```json
GET {{host}}/api/SubPrefeitura/Find
```

#### SubPrefeitura: Find - request parameters
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

#### SubPrefeitura: Find - server response
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

### SubPrefeitura: GetByCodigo - endpoint
```json
GET {{host}}/api/SubPrefeitura/GetByCodigo/{{codSubPrefeitura}}
```

#### SubPrefeitura: GetByCodigo - request parameters
```json
{
	"codSubPrefeitura" : "16"
}
```
| Atributo             | Obrigatório | Tipo   | Tamanho | Descrição
|----------------------| :---------: |--------|--------:|-------------
| **codSubPrefeitura** | sim         | string | 2       | Código de cada uma das 31 Subprefeituras (2003 a 2012)

> O atributo não tem nome. Deve ser passado o valor direamente na chamada da URL

#### SubPrefeitura: GetByCodigo - server response
```json
{
	"nome"   : "ARICANDUVA-FORMOSA-CARRAO",
	"codigo" : "26"
}
```
> Retorna o item encontrado. Se nenhum item for encontrado, retorna 404.

---

## Feira

### Feira: Find - endpoint
```json
GET {{host}}/api/Feira/Find?nome={{nome2find}}&codDistrito={{distrito2find}}&regiao5={{regiao5ToFind}}&bairro={{bairro2find}}
```

#### Feira: Find - request parameters
```json
{
	"nome"       : "PIRASSUNUNGA",
	"codDistrito": "01",
	"regiao5"    : 2,
	"bairro"     : "ALTO DA MOOCA"
}
```
| Atributo        | Obrigatório | Tipo   | Tamanho | Descrição
|-----------------| :---------: |--------|--------:|-------------
| **nome**        | yes         | string |     30  | Denominação da feira livre atribuída pela Supervisão de Abastecimento
| **codDistrito** | yes         | string |      9  | Código do Distrito Municipal conforme IBGE
| **regiao5**     | yes         | string |     --- | Região conforme divisão do Município em 5 áreas [Norte, Leste, Sul, Oeste, Centro]
| **bairro**      | yes         | string |     20  | Bairro de localização da feira livre


>A pesquisa pode ser feita por qualquer combinação dos parâmetros, desde que pelo menos um seja definido

#### Feira: Find - server response
```json
[
    {
		"nome"                : "AGUA BRANCA",
		"numeroRegistro"      : "5025-3",
		"setorCensitarioIBGE" : "355030848000070",
		"areaDePonderacaoIBGE": "3550308005059",
		"codDistrito"         : "48",
		"distrito"            : "LAPA",
		"codSubPrefeitura"    : "8",
		"subPrefeitura"       : "LAPA",
		"regiao5"             : "Oeste",
		"regiao8"             : "Oeste",
		"enderecoLogradouro"  : "RUA SILVEIRA RODRIGUES C/ MARIO",
		"enderecoNumero"      : "38.000000",
		"enderecoBairro"      : "VL ROMANA",
		"enderecoReferencia"  : "VESPASIANO E JESUINO BANDEIRA",
		"latitude"            : -23.532317,
		"longitude"           : -46.694256
    }
]
```
> Retorna um array com os itens encontrados. Se nenhum item for encontrado, retorna um array vazio.

---

### Feira: Add - endpoint
```json
POST {{host}}/api/Feira/Add
```

#### Feira: Add - request parameters
```json
{
	"nome"                 : "PIRASSUNUNGA",
	"numeroRegistro"       : "1013-8",
	"setorCensitarioIBGE"  : "355030801000054",
	"areaDePonderacaoIBGE" : "3550308005039",
	"codDistrito"          : "01",
	"codSubPrefeitura"     : "25",
	"regiao5"              : "LESTE",
	"regiao8"              : "leste1",
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
| **nome**                 | sim         | string |     30  | Denominação da feira livre atribuída pela Supervisão de Abastecimento
| **numeroRegistro**       | sim         | string |      6  | Número do registro da feira livre na PMSP
| **setorCensitarioIBGE**  | sim         | string |     15  | Setor censitário conforme IBGE
| **areaDePonderacaoIBGE** | sim         | string |     13  | Área de ponderação (agrupamento de setores censitários) conforme IBGE 2010
| **codDistrito**          | sim         | string |      9  | Código do Distrito Municipal conforme IBGE
| **codSubPrefeitura**     | sim         | string |      2  | Código de cada uma das 31 Subprefeituras (2003 a 2012)
| **regiao5**              | sim         | string |     --- | Região conforme divisão do Município em 5 áreas [Norte, Leste, Sul, Oeste, Centro]
| **regiao8**              | sim         | string |     --- | Região conforme divisão do Município em 8 áreas [Norte1, Norte2, Leste1, Leste2, Sul1, Sul2, Oeste, Centro]
| **enderecoLogradouro**   | sim         | string |     34  | Nome do logradouro onde se localiza a feira livre
| **enderecoNumero**       | não         | string |      5  | Um número do logradouro onde se localiza a feira livre
| **enderecoBairro**       | sim         | string |     20  | Bairro de localização da feira livre
| **enderecoReferencia**   | não         | string |     24  | Ponto de referência da localização da feira livre
| **latitude**             | sim         | number |     --- | Número de identificação do estabelecimento georreferenciado por SMDU/Deinfo
| **longitude**            | sim         | number |     --- | Longitude da localização do estabelecimento no território do Município, conforme MDC


#### Feira: Add - server response
```json
{
	"nome"                 : "PIRASSUNUNGA",
	"numeroRegistro"       : "1013-8",
	"setorCensitarioIBGE"  : "355030801000054",
	"areaDePonderacaoIBGE" : "3550308005039",
	"codDistrito"          : "01",
	"Distrito"             : "AGUA RASA",
	"codSubPrefeitura"     : "25",
	"SubPrefeitura"        : "MOOCA",
	"regiao5"              : "Leste",
	"regiao8"              : "Leste1",
	"enderecoLogradouro"   : "RUA TEREZINA",
	"enderecoNumero"       : "615",
	"enderecoBairro"       : "ALTO DA MOOCA",
	"enderecoReferencia"   : "CAMPO LARGO E MANAUS",
	"latitude"             : -23.564711,
	"longitude"            : -46.564711
}
```

---

### Feira: Edit - endpoint
```json
PUT {{host}}/api/feira/edit
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
| **nome**                 | sim         | string |     30  | Denominação da feira livre atribuída pela Supervisão de Abastecimento
| **numeroRegistro**       | sim         | string |      6  | Número do registro da feira livre na PMSP
| **setorCensitarioIBGE**  | sim         | string |     15  | Setor censitário conforme IBGE
| **areaDePonderacaoIBGE** | sim         | string |     13  | Área de ponderação (agrupamento de setores censitários) conforme IBGE 2010
| **codDistrito**          | sim         | string |      9  | Código do Distrito Municipal conforme IBGE
| **codSubPrefeitura**     | sim         | string |      2  | Código de cada uma das 31 Subprefeituras (2003 a 2012)
| **regiao5**              | sim         | string |     --- | Região conforme divisão do Município em 5 áreas [Norte, Leste, Sul, Oeste, Centro]
| **regiao8**              | sim         | string |     --- | Região conforme divisão do Município em 8 áreas [Norte1, Norte2, Leste1, Leste2, Sul1, Sul2, Oeste, Centro]
| **enderecoLogradouro**   | sim         | string |     34  | Nome do logradouro onde se localiza a feira livre
| **enderecoNumero**       | não         | string |      5  | Um número do logradouro onde se localiza a feira livre
| **enderecoBairro**       | sim         | string |     20  | Bairro de localização da feira livre
| **enderecoReferencia**   | não         | string |     24  | Ponto de referência da localização da feira livre
| **latitude**             | sim         | number |     --- | Número de identificação do estabelecimento georreferenciado por SMDU/Deinfo
| **longitude**            | sim         | number |     --- | Longitude da localização do estabelecimento no território do Município, conforme MDC

> A feira a ser atualizada será aquela referenciada pelo número de registro
>
> Somente os parâmetros passados serão atualizados

#### Edit Feira - server response
```json
{
	"nome"                 : "PIRASSUNUNGA",
	"numeroRegistro"       : "1013-8",
	"setorCensitarioIBGE"  : "355030801000054",
	"areaDePonderacaoIBGE" : "3550308005039",
	"codDistrito"          : "01",
	"Distrito"             : "AGUA RASA",
	"codSubPrefeitura"     : "25",
	"SubPrefeitura"        : "MOOCA",
	"regiao5"              : "Leste",
	"regiao8"              : "Leste1",
	"enderecoLogradouro"   : "RUA TEREZINA",
	"enderecoNumero"       : "615",
	"enderecoBairro"       : "ALTO DA MOOCA",
	"enderecoReferencia"   : "CAMPO LARGO E MANAUS",
	"latitude"             : -23.564711,
	"longitude"            : -46.564711
}
```



### Find Feira - endpoint
```json
GET {{host}}/api/api/feira/find
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
DELETE {{host}}/api/feira/remove
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
