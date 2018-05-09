import React from 'react';
import { shallow } from 'enzyme';
import Header from './Header';

const setup = () => {
    const wrapper = shallow(<Header />);

    return {
        wrapper,
    }
};

describe('Header Component', () => {
    it('should render without crashing', () => {
        const { wrapper } = setup();
        expect(wrapper).toMatchSnapshot();
    });
    
    it('should render three menu items', () => {
        const { wrapper } = setup();
        const menuItems = wrapper.find('li.nav-item');
        expect(menuItems.length).toBe(3);
    });
});