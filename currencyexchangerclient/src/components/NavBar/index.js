import React from 'react'
import { PrimaryNav, MenuLink, Menu } from './navElement'
const Navbar = () => {
  return (
    <>
      <PrimaryNav>
        <Menu>
          <MenuLink to="/exchange" activestyle>
            Currency Exchange
          </MenuLink>
          <MenuLink to="/chart" activestyle>
            Chart
          </MenuLink>
        </Menu>
      </PrimaryNav>
    </>
  )
}
export default Navbar