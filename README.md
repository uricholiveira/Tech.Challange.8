### Visão Geral do Projeto

Este repositório contém uma aplicação full-stack com um backend desenvolvido em .NET e um frontend em Nuxt 3. O projeto utiliza Docker Compose para facilitar o processo de configuração e execução dos serviços, incluindo o banco de dados, frontend, backend e um proxy reverso com Nginx.

### Estrutura do Projeto

- **Backend (.NET)**
  - Localizado na pasta `backend/`
  - Contém a API e a lógica de negócios da aplicação.
  
- **Frontend (Nuxt 3)**
  - Localizado na pasta `frontend/`
  - Responsável pela interface do usuário e interações no lado do cliente.
  
- **Nginx**
  - Configurado como um proxy reverso para direcionar o tráfego entre o frontend e o backend.

### Como Executar o Projeto

1. **Clone o repositório:**
   ```bash
   git clone https://github.com/seu-usuario/seu-repositorio.git
   cd seu-repositorio
   ```

2. **Configuração com Docker Compose:**

   Certifique-se de ter o Docker e o Docker Compose instalados.

   - Para construir e iniciar todos os serviços:
     ```bash
     docker-compose up --build
     ```

   - Para parar os serviços:
     ```bash
     docker-compose down
     ```

3. **Autenticação Padrão:**

   A autenticação padrão para acessar a aplicação é:
   - **Usuário:** demo
   - **Senha:** demo

### Logging e Monitoramento

Este projeto utiliza **OpenTelemetry**, **Grafana**, e **Loki** para monitoramento e análise de logs.

- **OpenTelemetry**: Coleta e exporta métricas e rastreamentos de desempenho.
- **Grafana**: Interface para visualização de métricas e logs.
- **Loki**: Sistema de agregação de logs, integrado ao Grafana.

Para acessar o Grafana:
- Abra `http://localhost:3000` no seu navegador.
- Use as credenciais de administrador configuradas para acessar.

### Estrutura do Docker Compose

O arquivo `docker-compose.yml` contém a configuração necessária para rodar os serviços:

- **frontend**: Serviço que roda a aplicação Nuxt 3.
- **backend**: Serviço que executa a API .NET.
- **nginx**: Proxy reverso que gerencia as requisições para o frontend e backend.
- **postgres**: Banco de dados utilizado pelo backend.
- **grafana** e **loki**: Serviços para monitoramento e análise de logs.

### Configuração de Logs

Para configurar o logging:

1. Certifique-se de que o **OpenTelemetry** esteja configurado no backend.
2. Os logs serão enviados para o Loki e poderão ser visualizados no Grafana.

### Conclusão

Este projeto oferece uma solução completa para o desenvolvimento de aplicações full-stack, com suporte a monitoramento avançado e logs centralizados. A autenticação padrão (demo/demo) facilita o acesso inicial à aplicação.
