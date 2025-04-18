import React from "react";
import { Container } from "react-bootstrap";

const Banner = () => {
    return (
        <div
            className="text-white shadow-sm mb-2 container-fluid"
            style={{
                background: "linear-gradient(to right, #007bff, #0056b3)"
            }}
        >

            <Container className="text-center">
                <h1 className="display-6 fw-bold m-0">Besmoke Scientific Inventory</h1>
            </Container>
        </div>
    );
};

export default Banner;