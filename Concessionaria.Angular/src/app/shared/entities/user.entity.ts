import { Register } from './register.entity';
import { Enumeration } from './enumeration.entity';

export class Usuario extends Register{
  name!: string;
  userName!: string;
  email!: string;
  password!: string;
  userRole!: Enumeration;
  idUserRole!: number;
}
