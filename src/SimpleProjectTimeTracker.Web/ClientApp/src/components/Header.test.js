import React from 'react';
import { render } from 'react-testing-library';
import { MemoryRouter } from 'react-router'
import Header from './Header';

describe('Header Component', () => {
    it('should render without crashing', () => {
        const { queryByText } = render(
            <MemoryRouter>
                <Header />
            </MemoryRouter>
        );
        const navbarBrand = queryByText('Simple Project Time Tracker');
        expect(navbarBrand.innerHTML).toBe('Simple Project Time Tracker');
    });
    
    it('should navigate to projects page when I click Projects link', () => {
        const { getByText } = render(
            <MemoryRouter>
                <Header />
            </MemoryRouter>
        );
        const menuLink = getByText('Projects');
        expect(menuLink.href).toMatch(/\/projects$/);
    });
});