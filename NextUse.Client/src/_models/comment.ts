import { Profile } from "./profile";
import { Product } from "./product";

export interface UserComment { // Comment er åbenbart et eksisterende interface i typescript, så vi ændrer lige navnet her
    id: number;
    content: string;
    createdAt: string;
    profileId?: number | null;
    profile?: Profile | null;
    productId?: number | null;
    product?: Product | null;
}