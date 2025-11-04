import { Role } from "../_utils/role.enum";

export interface User {
    id:string,
    username?: string,
    email: string,
    role:Role
}