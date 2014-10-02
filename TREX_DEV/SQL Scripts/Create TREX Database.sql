CREATE DATABASE TREX_DEV;
GO

USE TREX_DEV;

/*Create Trade table*/
CREATE TABLE Trade(
	[id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[stock] [varchar](50) NOT NULL,
	[strategyId] [varchar](50) NOT NULL,
	[when] [varchar](50) NOT NULL,
	[position] [varchar](50) NOT NULL,
	[buy] [bit] NOT NULL,
	[size] [int] NOT NULL,
	[price] [decimal](18, 4) NOT NULL,
	[auto] [bit] NOT NULL,
	[short] [bit] NOT NULL,
	[pnl] [decimal] (18,4) NOT NULL
);

/*Create Stock table*/
CREATE TABLE Stock(
	[symbol] [varchar](50) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[country] [varchar](50) NULL,
	[bid] [decimal](18, 4) NOT NULL,
	[ask] [decimal](18, 4) NOT NULL,
	[vol] [int] NOT NULL,
	[currency] [varchar](50) NOT NULL,
	[exchange] [varchar](50) NULL,
	[when] [varchar](50) NOT NULL,
	[open] [decimal](18, 4) NOT NULL,
	[prevClose] [decimal](18,4) NOT NULL,
	[change] [decimal](18, 4) NOT NULL,
	[changePercentage] [decimal](18, 4) NOT NULL,
	[summaryName] [varchar](50),
	[summaryValue] [varchar](50),
	[bidSize] [int] NOT NULL,
	[askSize] [int] NOT NULL,
	[marketCap] [decimal] 
	
);

/*Create Portfolio table*/
CREATE TABLE Portfolio(
	[stock] [varchar](50) NOT NULL,
	[incrementalBalance] [decimal](18, 4) NULL,
	[availableSize] [int] NULL,
	[totalSize] [int] NULL
);

/*Create Strategy table*/
CREATE TABLE Strategy(
	[id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[xmlConfig] [varchar](50) NULL,
	[type] [varchar](50) NOT NULL,
	[activated] [bit] NOT NULL,
	[name] [varchar](50) NOT NULL,
	[stock] [varchar](50) NOT NULL,
	[buy] [bit] NOT NULL,
	[short] [bit] NOT NULL,
	[size] [int] NOT NULL
);

/*Create foreign key relationships after all tables have been generated*/
-- ALTER TABLE Trade WITH CHECK ADD  CONSTRAINT [FK_Trade_Strategy] FOREIGN KEY([strategyId]) REFERENCES Strategy ([id]);

INSERT INTO Stock VALUES
('IBM', 'IBM', 'USA', '100.47', '100.89', '12000', 'USD', 'NYSE', '8/21/2014 10:05:00', '100.0', '100.0', '1.2', '0.2', 'IBM Summary', 'IBM Summary Value', '1000', '1000', '602000000000'),
('AAPL', 'Apple', 'USA', '110.50', '115.67', '7000', 'USD', 'NASDAQ', '8/21/2014 10:06:00', '100.0', '100.0', '1.6', '0.15', 'Apple Summary', 'Apple Summary Value', '1000', '1000', '598000000000' ),
('MSFT', 'Microsoft', 'USA', '100.47', '103.50', '8000', 'USD', 'NYSE', '8/21/2014 10:12:00', '100.0', '100.0', '3.5', '0.17', 'Microsoft Summary', 'Microsoft Summary Value', '1000', '1000', '600000000000' )

INSERT INTO Portfolio VALUES 
('IBM', '50.00', '1000', '1000'),
('AAPL', '-10.00', '2000', '2900'),
('MSFT', '80.00', '900', '1000')

INSERT INTO Strategy (xmlConfig, [type], activated, name, stock, buy, short, size ) VALUES 
('<strategy>BLABLABLA</strategy>', 'MOVING_AVERAGE', 1, 'TREX Strategy', 'AAPL', 1, 0, 2),
('<strategy>BLABLABLA</strategy>', 'BOLLINGER_BAND', 1, 'My Strategy', 'MSFT', 0, 1, 223),
('<strategy>BLABLABLA</strategy>', 'MOVING_AVERAGE', 0, 'Dino Algo', 'GOOG', 1, 1, 356)

INSERT INTO Trade (stock, strategyId, [when], position, buy, size, price, [auto], short, pnl)
VALUES

('IBM', 'MA_1', '8/21/2014 11:12:50', 'OPEN', '1', '100', '100.89', '1', '0', '-300'),
('AAPL', 'MA_2', '8/21/2014 11:13:50', 'OPEN', '1', '100', '101.89', '1', '0', '-500'),
('AAPL', 'MA_2', '8/21/2014 11:13:50', 'CLOSED', '0', '100', '121.89', '1', '0', '1000'),
('MSFT', 'BB_2', '8/21/2014 11:13:50', 'CLOSED', '0', '100', '161.89', '0', '0', '-100')

