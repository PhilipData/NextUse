import { Profile } from "./profile";

export interface Message {
    id: number;
    content: string;
    createdAt: string;
    fromProfileId?: number;
    fromProfile?: Profile | null;
    toProfileId?: number;
    toProfile?: Profile | null;
}