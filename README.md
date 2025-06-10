#  Clube da Leitura - Sistema de Gerenciamento 

##  Visão Geral
O **Clube da Leitura** é um sistema robusto desenvolvido em .NET para gerenciar todas as operações de um clube de leitura, desde o cadastro de membros até o controle de empréstimos de revistas. Com uma arquitetura em 3 camadas e seguindo boas práticas de POO, o sistema oferece uma solução completa e intuitiva.

##   Funcionalidades 

###  Módulo de Amigos
- **Cadastro Completo**:
  - Adicionar novos amigos com validação rigorosa
  - Editar informações existentes (nome, responsável, telefone)
  - Exclusão segura (apenas sem empréstimos ativos)

- **Validações Inteligentes**:
  - Formato de telefone: (XX) XXXX-XXXX ou (XX) XXXXX-XXXX
  - Nome e responsável: 3-100 caracteres
  - Bloqueio de duplicatas (nome + telefone)

###  Módulo de Caixas
- **Gestão de Coleções**:
  - Cadastro de novas caixas com etiqueta única
  - Personalização por cor 
  - Configuração de período padrão de empréstimo
- **Controle de Integridade**:
  - Impede exclusão de caixas com revistas
  - Validação de etiqueta (até 50 caracteres únicos)


###  Módulo de Revistas
- **Gestão Completa do Acervo**:
  - Cadastro com todos os metadados (título, edição, ano)
  - Atribuição automática à caixa correspondente
  - Controle de status (Disponível/Emprestada/Reservada)
- **Sistema Inteligente**:
  - Status padrão "Disponível" ao cadastrar
  - Bloqueio de duplicatas (título + edição)
  - Validação de campos (título 2-100 caracteres)


###  Módulo de Empréstimos
- **Fluxo Completo**:
  - Registro de novos empréstimos com data automática
  - Cálculo inteligente da data de devolução
  - Sistema de devolução com atualização de status
- **Controles Rigorosos**:
  - Limite de 1 empréstimo ativo por amigo
  - Validação de revistas disponíveis
  - Destaque visual para atrasos

##  Arquitetura 
- **Padrão de Projeto**: MVC com 3 Camadas
  - Camada de Apresentação 
  - Camada de Domínio 
  - Camada de Dados 


##  Tecnologias
 [![My Skills](https://skillicons.dev/icons?i=visualstudio,dotnet,cs,git,github,)](https://skillicons.dev)


## Requisitos

- .NET SDK (recomendado .NET 8.0 ou superior) para compilação e execução do projeto.
---

## Como executar

 #### Clone o Repositório
 ```
 git clone https://github.com/LedemarXavier/ClubeDaLeitura.App.git
 ```
 
 #### Navegue até a pasta raiz da solução
 ```
 cd ClubeDaLeitura.App
 ```
 
 #### Restaure as dependências
 ```
 dotnet restore
 ```
 
 #### Navegue até a pasta do projeto
 ```
 cd ClubeDaLeitura.App
 ```
 
 #### Execute o projeto
 ```
 dotnet run
 ````
 ----
