import React from 'react';

export default function ProductCard({ product, onUpdate }) {
    const handleUpdateClick = () => {
        onUpdate(product);
    };

    return (
        <div className="card mb-3">
            <div className="card-body">
                <h5 className="card-title">{product.productTypeName}</h5>
                <p className="card-text">Material: {product.productMaterialName}</p>
                <p className="card-text">Size: {product.productSizeName}</p>
                <p className="card-text">Available: {product.availableInventoryCount}</p>
                <button className="btn btn-sm btn-warning" onClick={handleUpdateClick}>
                    Update Product
                </button>
            </div>
        </div>
    );
}