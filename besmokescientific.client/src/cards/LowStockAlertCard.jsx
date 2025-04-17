import React from 'react';

export default function LowStockAlertCard({ product }) {
    if (!product || product.availableInventoryCount >= 50) return null;

    return (
        <div className="alert alert-danger" role="alert">
            <strong>Low Stock!</strong> {product.productTypeName} has only {product.availableInventoryCount} left.
        </div>
    );
}