# syntax = docker/dockerfile:1

ARG NODE_VERSION=22

FROM node:${NODE_VERSION}-alpine as builder
# Configurando o diretório de trabalho
WORKDIR /app

# Copiando os arquivos de configuração e dependências
COPY package*.json ./

# Instalando as dependências
RUN npm ci

# Copiando o restante da aplicação
COPY . .

# Expondo a porta
EXPOSE 8080
ENV NITRO_PORT=8080

# Compilando a aplicação
RUN npm run build

# Comando para iniciar a aplicação
ENTRYPOINT ["node", ".output/server/index.mjs"]