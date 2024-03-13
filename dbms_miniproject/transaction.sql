CREATE TABLE Transac(
     transaction_id INT PRIMARY KEY,
    purchase_quantity INT,
    product_id INT,
    amount DECIMAL(10, 2),
    CONSTRAINT FK_transaction_product FOREIGN KEY (product_id) REFERENCES product(product_id)
);
