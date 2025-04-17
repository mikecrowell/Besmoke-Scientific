const API_BASE = "/api";

async function safeFetch(url, options = {}) {
    const res = await fetch(url, { cache: "no-store", ...options });
    const contentType = res.headers.get("content-type");

    if (!res.ok) {
        const text = await res.text();
        throw new Error(`Error ${res.status}: ${text}`);
    }

    if (!contentType || !contentType.includes("application/json")) {
        const text = await res.text();
        throw new Error(`Expected JSON, got: ${text}`);
    }

    return await res.json();
}

export async function fetchProducts() {
    const response = await fetch(`${API_BASE}/products`); 
    if (!response.ok) {
        throw new Error('Failed to fetch products');
    }
    return await response.json();
}

export async function fetchProductTypes() {
    return await safeFetch(`${API_BASE}/producttypes`);
}

export async function fetchProductSizes() {
    const response = await fetch(`${API_BASE}/ProductSize`);
    if (!response.ok) {
        throw new Error('Failed to fetch product sizes');
    }
    return await response.json();
}

export async function fetchProductMaterials() {
    const response = await fetch(`${API_BASE}/ProductMaterial`);
    if (!response.ok) {
        throw new Error('Failed to fetch product materials');
    }
    return await response.json();
}

export async function fetchInventoryReport() {
    return await safeFetch(`${API_BASE}/inventory/report`);
}

export const createProduct = async (product) =>
    await fetch("/api/products", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(product),
    });

export const updateProduct = async (id, product) =>
    await fetch(`/api/products/${id}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(product),
    });

export const deleteProduct = async (id) =>
    await fetch(`/api/products/${id}`, {
        method: "DELETE"
    });

export const adjustInventory = async (productId, inAmount, outAmount) => {
    const response = await fetch(`${API_BASE}/inventory/adjust`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ productId, inAmount, outAmount })
    });

    if (!response.ok) {
        const message = await response.text();
        throw new Error(message || "Failed to adjust inventory");
    }

    return await response.json();
};

export const getAllProducts = async () => {
    const res = await fetch(`${API_BASE}/products`);
    return await res.json();
};

export const getInventoryStatus = async () => {
    const res = await fetch(`${API_BASE}/inventory/report`);
    return await res.json();
};