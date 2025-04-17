import React, { useState, useEffect } from "react";
import { createProduct, updateProduct, deleteProduct } from "../services/api";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";
import Card from "react-bootstrap/Card";

const ProductEditorCard = ({ products, productTypes, productSizes, productMaterials, onRefresh }) => {
    const [selectedProductId, setSelectedProductId] = useState("");
    const [formData, setFormData] = useState({
        type: "",
        size: "",
        material: "",
        availableInventoryCount: 0
    });

    useEffect(() => {
        if (selectedProductId) {
            const product = products.find(p => p.id === parseInt(selectedProductId));
            if (product) {
                setFormData({
                    type: product.type,
                    size: product.size,
                    material: product.material,
                    availableInventoryCount: product.availableInventoryCount
                });
            }
        } else {
            setFormData({
                type: "",
                size: "",
                material: "",
                availableInventoryCount: 0
            });
        }
    }, [selectedProductId]);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData(prev => ({ ...prev, [name]: value }));
    };

    const handleSave = async () => {
        if (selectedProductId) {
            await updateProduct(selectedProductId, formData);
        } else {
            await createProduct(formData);
        }
        setSelectedProductId("");
        onRefresh();
    };

    const handleDelete = async () => {
        if (selectedProductId) {
            await deleteProduct(selectedProductId);
            setSelectedProductId("");
            onRefresh();
        }
    };

    return (
        <Card className="mb-4 shadow">
            <Card.Header as="h5">Product Editor</Card.Header>
            <Card.Body>
                <Form.Group className="mb-3">
                    <Form.Label>Select Product (or leave blank to create new)</Form.Label>
                    <Form.Select
                        value={selectedProductId}
                        onChange={(e) => setSelectedProductId(e.target.value)}
                    >
                        <option value="">-- New Product --</option>
                        {products.map(p => (
                            <option key={p.id} value={p.id}>
                                {p.type} - {p.size} - {p.material}
                            </option>
                        ))}
                    </Form.Select>
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Product Type</Form.Label>
                    <Form.Select
                        name="type"
                        value={formData.type}
                        onChange={handleChange}
                    >
                        <option value="">-- Select --</option>
                        {productTypes.map(type => (
                            <option key={type.id} value={type.name}>{type.name}</option>
                        ))}
                    </Form.Select>
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Product Size</Form.Label>
                    <Form.Select
                        name="size"
                        value={formData.size}
                        onChange={handleChange}
                    >
                        <option value="">-- Select --</option>
                        {productSizes.map(size => (
                            <option key={size.id} value={size.name}>{size.name}</option>
                        ))}
                    </Form.Select>
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Product Material</Form.Label>
                    <Form.Select
                        name="material"
                        value={formData.material}
                        onChange={handleChange}
                    >
                        <option value="">-- Select --</option>
                        {productMaterials.map(material => (
                            <option key={material.id} value={material.name}>{material.name}</option>
                        ))}
                    </Form.Select>
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Available Inventory</Form.Label>
                    <Form.Control
                        type="number"
                        min="0"
                        name="availableInventoryCount"
                        value={formData.availableInventoryCount}
                        onChange={handleChange}
                    />
                </Form.Group>

                <Button variant="success" className="me-2" onClick={handleSave}>
                    {selectedProductId ? "Update Product" : "Add Product"}
                </Button>
                {selectedProductId && (
                    <Button variant="danger" onClick={handleDelete}>
                        Delete Product
                    </Button>
                )}
            </Card.Body>
        </Card>
    );
};

export default ProductEditorCard;