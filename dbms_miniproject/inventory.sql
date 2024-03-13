CREATE TABLE inventory (
    inventory_id INT PRIMARY KEY,
    availability INT,
    warehouse_id INT,
    product_id INT,
    CONSTRAINT FK_inventory_product FOREIGN KEY (product_id) REFERENCES product(product_id),
    CONSTRAINT FK_inventory_warehouse FOREIGN KEY (warehouse_id) REFERENCES warehouse(warehouse_id)
);
