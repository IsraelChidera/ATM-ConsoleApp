CREATE DATABASE TestExDb

CREATE TABLE Customers(
 CustomerID int Primary Key Identity(1,1),
 CardNumber varchar(50) NOT NULL,
Pin varchar(50) NOT NULL,
LogTime varchar(50)
);

INSERT INTO Customers(CardNumber, Pin, LogTime)
VALUES('60677722', '0190', GETDATE()),
('00921134', '9021', GETDATE()),
('67221121', '5507', GETDATE())

CREATE TABLE Withdraw(
  WithdrawID int Primary Key Identity(1,1),
  Balance INT,  
  AmountWithdrawn INT,
  FOREIGN KEY (WithdrawID) REFERENCES Customers(CustomerID)
);

INSERT INTO Withdraw( Balance, AmountWithdrawn)
VALUES(967666, 980),
( 267666, 99980),
(1067666, 80)

CREATE TABLE Deposit(
  DepositID int Primary Key Identity(1,1),
  AmountDeposited INT,
  DepositDescription varchar(50),
  FOREIGN KEY (DepositID) REFERENCES Customers(CustomerID)
);

INSERT INTO Deposit( AmountDeposited, DepositDescription)
VALUES(9067666, 'Money for food'),
( 2007666, 'Kroft payment'),
(5007666, 'Savings for car')

CREATE TABLE Transfer(
  TransferID int Primary Key Identity(1,1),  
  ReceiverAccount varchar(50),
  AmountTransferred varchar(50),
  TransferDescription varchar(50),
  CreatedAt varchar(50),  
  FOREIGN KEY (TransferID) REFERENCES Customers(CustomerID)
);

INSERT INTO Transfer( ReceiverAccount, AmountTransferred, TransferDescription, CreatedAt)
VALUES('90676660', '600', 'Money for food', GETDATE()),
( '20076660', '465','Kroft payment', GETDATE()),
('50076660', '39', 'Savings for car', GETDATE());


CREATE VIEW TransactionView AS
SELECT customer.CardNumber, customer.CustomerID, customer.Pin, customer.LogTime, deposit.AmountDeposited, deposit.DepositDescription, 
transfer.ReceiverAccount, transfer.AmountTransferred, transfer.TransferDescription, transfer.CreatedAt, withdraw.Balance, withdraw.AmountWithdrawn
 FROM Customers customer
 INNER JOIN Deposit deposit ON customer.CustomerID = deposit.DepositID
 INNER JOIN Transfer transfer ON customer.CustomerID = transfer.TransferID
 INNER JOIN Withdraw withdraw ON customer.CustomerID = withdraw.WithdrawID;


