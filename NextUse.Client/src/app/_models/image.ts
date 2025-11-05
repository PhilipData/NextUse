import { Product } from "./product";

export interface Image {
    id: number;
    blob: string;
    blobUrl: string;
    productId: number;
    product: Product | null;
}