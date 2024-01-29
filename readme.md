# Batch

Uma aplicação simples de console que realiza um CRUD no banco de dados a partir da leitura de arquivos .csv, usando o modelo worker-job.

* Worker: instância que coordena uma fila de trabalhos a serem executados em ordem de chegada (FIFO)
* Job: instância que representa uma operação ou trabalho a ser executado

## Tecnologias utilizadas

* .NET Core - Console application
* SQLITE - Database
* Dapper - Micro ORM
* CsvHelper - Reading and write of files .csv

## Camadas

O Batch é modulado em diferentes camadas com suas respectivas responsabilidades. 
* Domain: contém os modelos, entidades e enumerações que representam os dados manipulados pela aplicação.
* Repository: contém as classes e interfaces que manipulam a conexão com o banco de dados. O banco de dados é manipulado usando o micro-ORM Dapper. 
* Services: contém a implementação dos serviços que são utilizados pela aplicação:
    * LocalFiles: manipulação dos arquivos locais com System.IO.File
    * CsvService: conversão de arquivos .csv em objetos usando CsvHelper
* Application: contém a implementação da lógica de negócio da aplicação e uso dos serviços implementados:
    * FileHandler: manipulação do LocalFiles e CsvService
    * Worker: enfileiramento e execução de jobs
    * Jobs: operações CRUD a serem executadas
* Extensions: contém as configurações de injeção de dependências da aplicação
* Configurations: contém os arquivos de configuração da aplicação

## Banco de dados da aplicação

A aplicação utiliza o SQLite para persistência e gerenciamento dos dados.

O esquema do banco de dados contém as seguintes tabelas:
* Products: registra tipos de produtos
    * ProductId, BarCode, ProductName, SupplierId, Inventory, Price
* Suppliers: registra fornecedores de produtos
    * SupplierId, SupplierName, ActiveContract

## Layout dos arquivos

Arquivos que são lidos:

* YYYYMMDD_VERSION_products_create.csv: dados para cadastrar novos produtos
* YYYYMMDD_VERSION_products_update.csv: dados para atualizar produtos
* YYYYMMDD_VERSION_products_delete.csv: dados para remoção de produtos

Arquivos que são gravados:

* YYYYMMDD_VERSION_products_view.csv: dados representando os produtos cadastrados em uma determinada data


Os arquivos são sempre buscados no diretório files/input e movidos depois do processamento para o diretório files/output. 