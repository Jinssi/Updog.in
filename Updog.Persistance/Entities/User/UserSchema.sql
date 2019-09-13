CREATE TABLE User (
    Id SERIAL NOT NULL PRIMARY KEY,
    Username VARCHAR(24) NOT NULL UNIQUE INDEX,
    Email VARCHAR(64) UNIQUE,
    PasswordHash CHAR(60) NOT NULL,
    JoinedDate TIMESTAMP,
    PostKarma INT,
    CommentKarma INT
);