CREATE TABLE users(
	idUser INT PRIMARY KEY IDENTITY (1, 1) NOT NULL,
	username NVARCHAR(150),
	password NVARCHAR(150),
	email NVARCHAR(150),
	name NVARCHAR(150), 
	birthday DATE NULL, 
	address  NVARCHAR(MAX),
	gender INT NULL,
	phone NVARCHAR(15),
	slide BIT,
	recoverPasswordCode varchar(36), 
	imgUrl NVARCHAR(150),
	newsletter BIT NULL, 
	lastSign DATE, 
	role INT NULL,
	addDate DATE, 
	lastUpdate DATE,
	active BIT NULL
)

CREATE TABLE products(
	idProduct INT PRIMARY KEY IDENTITY (1, 1) NOT NULL,
	productName NVARCHAR(150),
	shortDescription NVARCHAR(150),
	longDescription NVARCHAR(MAX) NULL,
	brand NVARCHAR(150), 
	category NVARCHAR(50), 
	stock INT NULL,
	warnings NVARCHAR(300),
	imgUrl NVARCHAR(150),
	addDate DATE, 
	lastUpdate DATE,
	active BIT NULL
)

CREATE TABLE orders(
	idOrder INT  PRIMARY KEY IDENTITY (1, 1) NOT NULL,
	idUser INT FOREIGN KEY REFERENCES users(idUser),
	products NVARCHAR(MAX) NULL,
	qtd NVARCHAR(MAX),
	paid BIT NULL,
	addDate DATE
)

CREATE TABLE comments(
	idComment INT PRIMARY KEY IDENTITY (1, 1) NOT NULL,
	idUser INT  FOREIGN KEY REFERENCES users(idUser),
	idProduct INT  FOREIGN KEY REFERENCES products(idProduct),
	idComments INT NULL,
	rating INT NULL, 
	title NVARCHAR(150), 
	comment NVARCHAR(1000),
	addDate DATE,
	lastUpdate DATE,
	active BIT NULL
)

CREATE TABLE categories {
	idCategory INT PRIMARY KEY IDENTITY (1, 1) NOT NULL,
	name NVARCHAR(100),
	refs INT
}