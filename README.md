# Documentação do Projeto

## Integrantes:

### Gabriel Cavalcante
### Renan Henrique de Oliveira

## 1. Introdução

### Visão Geral do Projeto
Desenvolvimento de uma API em .NET 5 para gerenciar uma aplicação de gestão de livros, incluindo funcionalidades como autenticação de usuários e gerenciamento de produtos.

### Objetivos e Propósito do Sistema
Criar uma API robusta, segura e escalável que permita operações CRUD sobre entidades de usuário e produto, além de fornecer autenticação baseada em JWT.

### Benefícios Esperados do Projeto
- Facilidade de manutenção
- Segurança aprimorada
- Escalabilidade
- Integração simplificada com frontend, backend e database

## 2. Visão Geral do Sistema

### Descrição do Sistema
API RESTful para um sistema de gestão, oferecendo endpoints para autenticação de usuários e gerenciamento de livros.

### Público-Alvo do Sistema
Administradores do sistema e clientes finais da gestão, como os funcionários para auxílio e rapidez no atendimento dos clientes.

### Requisitos Funcionais e Não Funcionais
O sistema deve ser seguro, escalável, com alta disponibilidade e desempenho.

## 3. Arquitetura do Sistema

### Explicação da Arquitetura MVC (Model-View-Controller)
A arquitetura MVC separa a aplicação em três componentes principais:
- **Models**: Dados e lógica de negócios.
- **Views**: Interfaces de usuário (não aplicável diretamente em uma API).
- **Controllers**: Intermediários que tratam as requisições e coordenam as respostas.

### Papel de Cada Componente
- **Models**: Representam as entidades e a lógica de negócios.
- **Controllers**: Gerenciam a lógica de entrada do usuário e interagem com os modelos.
- **Views**: Não aplicável diretamente em uma API pois ela fornece dados e não interfaces gráficas.

### Uso do Padrão Repository para Acesso a Dados
Repositórios encapsulam a lógica de acesso a dados, proporcionando uma abstração sobre a interação com o banco de dados.

## 4. Requisitos Funcionais

### Lista Detalhada de Funcionalidades do Sistema
- Autenticação de usuários com JWT
- Operações CRUD para usuários, produtos e pedidos
- Consultas filtradas e paginadas de produtos

### Casos de Uso Principais
- Usuário registra uma conta
- Usuário faz login e recebe um token JWT
- Administrador adiciona, edita ou remove livros com JWT
- Usuário adiciona, edita ou remove livros com JWT

## 5. Requisitos Não Funcionais

### Desempenho Esperado do Sistema
- Respostas rápidas às requisições
- Suporte a alta carga de usuários simultâneos

### Segurança e Autenticação
- Implementação de autenticação JWT
- Proteção contra ataques comuns como SQL Injection e Cross-Site Scripting (XSS)

### Escalabilidade e Manutenibilidade
Arquitetura modular para facilitar a manutenção e a escalabilidade horizontal.

## 6. Tecnologias Utilizadas

- **Linguagens de Programação**: C# e TypeScript
- **Frameworks**: .NET 5 para backend, Entity Framework Core para ORM, React
- **Bancos de Dados**: SQL Server ou PostgreSQL
- **Ferramentas de Desenvolvimento**: Visual Studio, Docker, Git

## 7. Modelo de Dados

### Estrutura do Banco de Dados
Tabelas para `Users`, `Book` e `Gender`.

### Relacionamentos entre Entidades
Relacionamentos como muitos para muitos entre `Book` e `Gender`.

### Esquema de Armazenamento
O banco de dados será configurado inicialmente com todas as tabelas e relacionamentos necessários para o funcionamento da aplicação, e os dados serão enviados para o banco de dados e acessados quando necessário.

## 8. Interfaces do Usuário

### Layout e Design das Interfaces
As interfaces de usuário serão desenvolvidas utilizando React.js, seguindo as melhores práticas de design e usabilidade. A estrutura dos dados JSON retornados pela API será considerada durante o desenvolvimento das interfaces.

### Funcionalidades Específicas de Cada Tela
Cada tela terá funcionalidades específicas relacionadas à sua finalidade no contexto do sistema de e-commerce, como exibição de produtos, detalhes do carrinho de compras, finalização de pedidos, etc.

### Fluxos de Interação do Usuário
Os fluxos de interação do usuário serão definidos de acordo com as necessidades de cada tela, com base nos endpoints bem definidos pela API para cada ação do usuário, garantindo uma experiência de usuário fluida e intuitiva.

## 9. Arquitetura de Implementação

### Organização do Código-Fonte
- **Versionamento Semântico**
- **Padrões de Commit Semântico**
- **Models**: Classes representando as entidades.
- **Repositories**: Interfaces e implementações para acesso a dados.
- **Services (Business)**: Lógica de negócios.
- **Controllers**: Endpoints da API.

### Divisão em Módulos e Componentes
Separação clara entre modelos, repositórios, serviços e controladores.

### Dependências entre os Componentes
Controladores dependem dos serviços, que por sua vez dependem dos repositórios.

## 10. Planejamento de Implantação

### Ambientes de Implantação (Dev, Teste, Produção)
Configuração de diferentes ambientes com variáveis de ambiente específicas.

### Procedimentos de Implantação
- Uso de Docker para criar imagens e contêineres
- CI/CD para automação

### Migração de Dados se Necessário
Uso de migrations para atualização do banco de dados sem perda de dados.

## 11. Gestão de Configuração e Controle de Versão

### Políticas de Controle de Versão
Estratégia de branching e versionamento semântico.

### Ramificação do Código-Fonte
Uso de Git Flow ou similar para gerenciar branches de desenvolvimento e releases.

### Uso de Ferramentas de Controle de Versão
Git, hospedado em plataformas como GitHub ou GitLab.

## 12. Gestão de Projetos

### Cronograma de Desenvolvimento
Desenvolvimento da API e banco durante as duas primeiras semanas e nas últimas, desenvolvimento do frontend e correção de bugs.

### Atribuição de Tarefas e Responsabilidades
Equipe com papéis definidos, ambos com o desenvolvimento fullstack, atribuindo partes específicas para cada desenvolvedor de acordo com o grau de compreensão da parte escolhida.

### Monitoramento do Progresso do Projeto
Uso de ferramentas como Trello ou GitHub para acompanhamento de tarefas.

## 13. Considerações de Segurança

### Mecanismos de Autenticação e Autorização
Implementação de JWT para autenticação, políticas de autorização baseadas em roles.

### Proteção contra Vulnerabilidades Conhecidas
Validação de entrada, proteção contra injeção de SQL, XSS, CSRF, etc.

### Auditoria e Registro de Atividades Sensíveis
Logging e monitoramento de ações críticas no sistema.

## 14. Considerações de Manutenção

### Planos de Suporte Pós-Implantação
Definição de SLA para suporte e manutenção.

### Processo de Correção de Bugs e Implementação de Melhorias
Procedimentos claros para reporte e resolução de bugs, bem como para sugestões de melhorias.

### Atualizações de Segurança e de Software
Políticas para atualização regular de dependências e patches de segurança.
