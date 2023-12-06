
DROP SCHEMA IF EXISTS webshop CASCADE;
CREATE SCHEMA IF NOT EXISTS webshop;
DROP TABLE IF EXISTS webshop.password_hash;
DROP TABLE IF EXISTS webshop.customer_buy;
DROP TABLE IF EXISTS webshop.avatar;
DROP TABLE IF EXISTS webshop.ordre;
DROP TABLE IF EXISTS webshop.users;

create table webshop.users
(
    user_id         SERIAL PRIMARY KEY,
    full_name  VARCHAR(50)  NOT NULL,
    email      VARCHAR(50)  NOT NULL UNIQUE,
    admin      VARCHAR(20)      NOT NULL
);

create table webshop.avatar
(
    avatar_id     SERIAL PRIMARY KEY,
    avatar_name  VARCHAR(50)  NOT NULL,
    avatar_price        integer   not NULL,
    information VARCHAR(300),
    deleted bool not null DEFAULT FALSE
);

INSERT INTO webshop.avatar (avatar_name,avatar_price, information, deleted)
VALUES ('Nummer1', 10, null, false);

create table webshop.order
(
    order_id SERIAL PRIMARY KEY,
    user_id INTEGER NOT NULL,
    FOREIGN KEY (user_id) REFERENCES webshop.users (user_id)
);

create table webshop.customer_buy
(
    customer_buy_id SERIAL PRIMARY KEY,
    order_id INTEGER NOT NUll,
    avatar_id INTEGER NOT NUll,
    FOREIGN KEY (order_id) REFERENCES webshop.order (order_id),
    FOREIGN KEY (avatar_id) REFERENCES webshop.avatar (avatar_id)
);

create table webshop.password_hash
(
    user_id   integer      NOT NULL,
    hash      VARCHAR(350) NOT NULL,
    salt      VARCHAR(180) NOT NULL,
    FOREIGN KEY (user_id) REFERENCES webshop.users (user_id)
);