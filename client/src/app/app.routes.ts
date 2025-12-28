import { Routes } from '@angular/router';
import { Home } from '../features/home/home';
import { MemberList } from '../features/members/member-list/member-list';
import { MemberDetailed } from '../features/members/member-detailed/member-detailed';
import { Lists } from '../features/lists/lists';
import { Messages } from '../features/messages/messages';
import { authGuard } from '../core/guards/auth-guard';

export const routes: Routes = [
    { path: '', component: Home, title: 'Home' },
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [authGuard],
        children: [
            { path: 'members', component: MemberList, title: 'Members' },
            { path: 'members/:id', component: MemberDetailed, title: 'Member Details' },
            { path: 'lists', component: Lists, title: 'Lists' },
            { path: 'messages', component: Messages, title: 'Messages' },
        ]
    },

    { path: '**', component: Home, title: 'Home' }
];
