import React from "react";
import Card from "react-bootstrap/Card";
import Button from "react-bootstrap/Button";

const SideMenuCard = ({ selectedView, onSelectView }) => {
    return (
        <Card className="mb-4 shadow">
            <Card.Body>
                <Button
                    variant={selectedView === "report" ? "primary" : "outline-primary"}
                    onClick={() => onSelectView("report")}
                    className="w-75 mb-2"
                >
                    Report
                </Button>
                <Button
                    variant={selectedView === "inventory" ? "primary" : "outline-primary"}
                    onClick={() => onSelectView("inventory")}
                    className="w-75 mb-2"
                >
                    Inventory
                </Button>
                <Button
                    variant={selectedView === "products" ? "primary" : "outline-primary"}
                    onClick={() => onSelectView("products")}
                    className="w-75"
                >
                    Products
                </Button>
            </Card.Body>
        </Card>
    );
};

export default SideMenuCard;