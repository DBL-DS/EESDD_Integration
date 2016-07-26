CREATE TABLE admin (
	name			TEXT NOT NULL UNIQUE,
	password		TEXT NOT NULL,
	realName		TEXT NOT NULL,
	regDate			TIMESTAMP NOT NULL DEFAULT (datetime('now','localtime')),
	lastDate		TIMESTAMP NOT NULL DEFAULT (datetime('now','localtime')),
	grantUserName	Text NOT NULL,
	PRIMARY KEY(name)
);

CREATE TABLE regular (
	name		TEXT NOT NULL UNIQUE,
	password	TEXT,
	realName	TEXT NOT NULL,
	regDate		TIMESTAMP NOT NULL DEFAULT (datetime('now','localtime')),
	lastDate	TIMESTAMP NOT NULL DEFAULT (datetime('now','localtime')),
	gender		TEXT NOT NULL,
	height		REAL NOT NULL,
	weight		REAL NOT NULL,
	age			INTEGER NOT NULL,
	driAge		INTEGER NOT NULL,
	career		TEXT NOT NULL,
	contact		TEXT NOT NULL,
	exp			TEXT NOT NULL,
	PRIMARY KEY(name)
);

INSERT INTO admin (name, password, realName, grantUserName) VALUES("monicang", "ac577acd3df150aea6493d07c66116b1", "模拟舱", "monicang");