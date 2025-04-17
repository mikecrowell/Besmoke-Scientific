import React, { useState } from "react";
import { adjustInventory } from "../services/api";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";
import Card from "react-bootstrap/Card";

const AdjustInventoryCard = ({ products, onInventoryAdjusted }) => {
    const [productId, setProductId] = useState("");
    const [inAmount, setInAmount] = useState(0);
    const [outAmount, setOutAmount] = useState(0);
    const [error, setError] = useState("");

    const handleAdjust = async () => {
        const parsedProductId = parseInt(productId);

        if (isNaN(parsedProductId)) {
            setError("Please select a valid product.");
            return;
        }

        if (isNaN(inAmount) || isNaN(outAmount)) {
            setError("Inventory values must be numbers.");
            return;
        }

        if (inAmount <= 0 && outAmount <= 0) {
            setError("Please enter a value greater than 0 for In or Out.");
            return;
        }

        try {
            await adjustInventory(parsedProductId, inAmount, outAmount);
            onInventoryAdjusted(); 
            setInAmount(0);
            setOutAmount(0);
            setProductId("");
            setError("");
        } catch (err) {
            console.error("Failed to adjust inventory:", err);
            setError("Failed to adjust inventory. Please try again.");
        }
    };

    return (
        <Card className="mb-4 shadow">
            <Card.Header as="h5">Adjust Inventory</Card.Header>
            <Card.Body>
                {error && <p className="text-danger">{error}</p>}

                <Form.Group>
                    <Form.Label>Select Product</Form.Label>
                    <Form.Select
                        value={productId}
                        onChange={(e) => setProductId(e.target.value)}
                        className="mb-3"
                    >
                        <option value="">-- Select --</option>
                        {products.map(p => (
                            <option key={p.id} value={p.id}>
                                {p.type} - {p.size} - {p.material}
                            </option>
                        ))}
                    </Form.Select>
                </Form.Group>

                <Form.Group>
                    <Form.Label>Inventory In</Form.Label>
                    <Form.Control
                        type="number"
                        min="0"
                        value={inAmount}
                        onChange={(e) => setInAmount(parseInt(e.target.value) || 0)}
                        className="mb-2"
                    />
                </Form.Group>

                <Form.Group>
                    <Form.Label>Inventory Out</Form.Label>
                    <Form.Control
                        type="number"
                        min="0"
                        value={outAmount}
                        onChange={(e) => setOutAmount(parseInt(e.target.value) || 0)}
                        className="mb-3"
                    />
                </Form.Group>

                <Button variant="primary" onClick={handleAdjust} disabled={!productId}>
                    Submit Adjustment
                </Button>
            </Card.Body>
        </Card>
    );
};

export default AdjustInventoryCard;