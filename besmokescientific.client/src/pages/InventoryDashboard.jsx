import React, { useEffect, useState } from "react";
import InventoryReportCard from "../cards/InventoryReportCard";
import Container from "react-bootstrap/Container";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import Form from "react-bootstrap/Form";
import Banner from "../layout/Banner";
import {
    fetchProductTypes,
    fetchProducts,
    fetchProductSizes,
    fetchProductMaterials,
} from "../services/api";
import AdjustInventoryCard from "../cards/AdjustInventoryCard";
import ProductEditorCard from "../cards/ProductEditorCard";
import SideMenuCard from "../cards/SideMenuCard";

const InventoryDashboard = () => {
    const [products, setProducts] = useState([]);
    const [productTypes, setProductTypes] = useState([]);
    const [productSizes, setProductSizes] = useState([]);
    const [productMaterials, setProductMaterials] = useState([]);
    const [selectedType, setSelectedType] = useState("");
    const [selectedView, setSelectedView] = useState("report"); // default view

    const loadProducts = async () => {
        try {
            const all = await fetchProducts();
            setProducts(all);
        } catch (err) {
            console.error("Failed to load products:", err);
        }
    };

    const loadTypes = async () => {
        try {
            const types = await fetchProductTypes();
            setProductTypes(types);
        } catch (err) {
            console.error("Failed to load product types:", err);
        }
    };

    const loadSizes = async () => {
        try {
            const sizes = await fetchProductSizes();
            setProductSizes(sizes);
        } catch (err) {
            console.error("Failed to load product sizes:", err);
        }
    };

    const loadMaterials = async () => {
        try {
            const materials = await fetchProductMaterials();
            setProductMaterials(materials);
        } catch (err) {
            console.error("Failed to load product materials:", err);
        }
    };

    useEffect(() => {
        loadTypes();
        loadProducts();
        loadSizes();
        loadMaterials();
    }, []);

    return (
        <div className="page-wrapper">            
            <Container fluid className="mt-4">
                <Row>
                    <Col>
                        <Banner />
                    </Col>
                </Row>
                <Row>
                    <Col md={3}>
                        <SideMenuCard
                            selectedView={selectedView}
                            onSelectView={setSelectedView}
                        />
                    </Col>
                    <Col md={9}>
                        {selectedView === "report" && (
                            <Container className="text-center">
                                <Form.Group className="mb-3" controlId="productTypeFilter">
                                    <Form.Label>Filter by Product Type</Form.Label>
                                    <Form.Select
                                        value={selectedType}
                                        onChange={(e) => setSelectedType(e.target.value)}
                                        className="w-50 mx-auto"
                                    >
                                        <option value="">All Types</option>
                                        {productTypes.map((type) => (
                                            <option key={type.id} value={type.name}>
                                                {type.name}
                                            </option>
                                        ))}
                                    </Form.Select>
                                </Form.Group>

                                <InventoryReportCard
                                    products={products}
                                    filterType={selectedType}
                                />
                            </Container>
                        )}

                        {selectedView === "inventory" && (
                            <>
                                <h4 className="text-center">Adjust Inventory</h4>
                                <AdjustInventoryCard
                                    products={products}
                                    onInventoryAdjusted={loadProducts}
                                />
                            </>
                        )}

                        {selectedView === "products" && (
                            <>
                                <h4 className="text-center">Manage Products</h4>
                                <ProductEditorCard
                                    products={products}
                                    productTypes={productTypes}
                                    productSizes={productSizes}
                                    productMaterials={productMaterials}
                                    onRefresh={loadProducts}
                                />
                            </>
                        )}
                    </Col>
                </Row>
            </Container>
        </div>
    );
};

export default InventoryDashboard;