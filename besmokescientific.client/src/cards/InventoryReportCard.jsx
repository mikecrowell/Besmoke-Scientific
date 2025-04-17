import React from "react";
import Card from "react-bootstrap/Card";
import Table from "react-bootstrap/Table";
import Alert from "react-bootstrap/Alert";

const InventoryReportCard = ({ products, filterType }) => {
    const filteredProducts = filterType
        ? products.filter(p => p.type === filterType)
        : products;

    return (
        <Card className="mb-4 shadow">
            <Card.Header as="h5">Inventory Report</Card.Header>
            <Card.Body>
                <Table striped bordered hover responsive>
                    <thead>
                        <tr>
                            <th>Type</th>
                            <th>Size</th>
                            <th>Material</th>
                            <th>Available Inventory</th>
                        </tr>
                    </thead>
                    <tbody>
                        {filteredProducts.map((p) => (
                            <tr
                                key={p.id}
                                className={p.availableInventoryCount < 50 ? "table-danger" : ""}
                            >
                                <td>{p.type}</td>
                                <td>{p.size}</td>
                                <td>{p.material}</td>
                                <td>{p.availableInventoryCount}</td>
                            </tr>
                        ))}
                    </tbody>
                </Table>
                {filteredProducts.some((p) => p.availableInventoryCount < 50) && (
                    <Alert variant="danger">
                        Warning: Some products have less than 50 in stock!
                    </Alert>
                )}
            </Card.Body>
        </Card>
    );
};

export default InventoryReportCard;