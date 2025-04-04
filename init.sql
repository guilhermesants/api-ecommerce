CREATE DATABASE db_ecommerce;

\connect db_ecommerce;

CREATE EXTENSION IF NOT EXISTS "pgcrypto";

CREATE TABLE categorias (
	id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
	nome VARCHAR(255) NOT NULL
);

CREATE TABLE produtos (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    nome VARCHAR(255) NOT NULL,
    valor DECIMAL(10,2) NOT NULL,
    qtd_estoque INT NOT NULL,
    url_imagem VARCHAR(255) NULL,
    data_cadastro TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    data_alteracao TIMESTAMP NULL,
    id_categoria UUID NOT NULL,
    ativo BOOLEAN NOT NULL DEFAULT TRUE,
    CONSTRAINT fk_produtos_categoria FOREIGN KEY (id_categoria) REFERENCES categorias(id)
);