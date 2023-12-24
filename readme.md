# Batch

Exemplo de aplicação de console para manipulação de banco de dados utilizando o micro-orm Dapper para um CRUD a partir de arquivos.

## Camadas

O Batch é modulado em diferentes camadas com suas respectivas responsabilidades. 
* Domain: contém os modelos e entidades que representam os dados manipulados pela aplicação.
    * /models: contém os modelos que representam as tabelas do banco de dados manipulados pela aplicação.
    * /entities: entidades que representam o formato dos dados dos arquivos csv manipulados pela aplicação.
* Repository: contém as classes e interfaces que manipulam a conexão com o banco de dados. 
* Services: contém a implementação dos serviços que são utilizados pela aplicação. 
* Application: contém a implementação da lógica de negócio da aplicação.
* Extensions: contém as configurações de injeção de dependências da aplicação.
* Configurations: contém os arquivos de configuração da aplicação.
* Logging: contém os arquivos de log da aplicação.
## Contexto da aplicação

O batch foi desenvolvido a pedido de uma rede de supermercados chamada Good-Price.

O sistema de caixa do supermercado possui o seu próprio banco de dados, mas o gerente deseja armazenar alguns dados em outro repositório para fazer estudos e análises sobre os produtos. Então, diariamente o sistema de caixa gera arquivos no formato csv dos novos produtos cadastrados, de alterações de preços, estoques e fornecedores dos produtos.

## Banco de dados da aplicação

A aplicação utiliza o SGBD SQLite para persistência e gerenciamento dos dados que poderão ser analisados pelo gerente.

O esquema do banco de dados contém as seguintes tabelas:
* Products: armazena os registros dos produtos que são comercializados pelas unidades do supermercado.  
Contém as seguintes colunas:
    * ProductId, BarCode, ProductName, SupplierId, Inventory, Price
* Suppliers: armazena os registros dos fornecedores que vendem os produtos para o supermercado.  
Contém as seguintes colunas:
    * SupplierId, SupplierName, ActiveContract

## Layout dos arquivos

O Batch realizará a leitura dos arquivos no formato csv.  
Os seguintes arquivos são disponibilizados pelo sistema de caixa:
* YYYYMMDD_VERSION_products_update.csv: contém os dados atuais dos produtos do sistema de caixa que precisam ser atualizados
    * Cabeçalho: ProductId, ProductName, SupplierId, Inventory, Price
* YYYYMMDD_VERSION_products_create.csv: contém os dados de novos produtos do sistema de caixa que foram registrados
    * Cabeçalho: ProductId, BarCode, ProductName, SupplierId, Inventory, Price
* YYYYMMDD_VERSION_products_delete.csv: contém os dados de novos produtos do sistema de caixa que foram deletados
    * Cabeçalho: ProductId

Os arquivos são sempre buscados no diretório files/input e movidos depois do proessamento para o diretório files/output. 